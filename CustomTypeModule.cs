using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Data;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Configuration;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static HotChocolate.Language.Utf8GraphQLParser;

namespace HotChocolateNotCallingConfigure
{
    public class CustomTypeModule : ITypeModule
    {
        private ExtendSchemaIn ExtendSchemaIn { get; set; }
        public event EventHandler<EventArgs> TypesChanged;

        public bool UpdateSchema(ExtendSchemaIn extendSchemaIn)
        {
            ExtendSchemaIn = extendSchemaIn;
            TypesChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public async ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(IDescriptorContext context, CancellationToken cancellationToken)
        {
            var dynamicTypes = new List<ITypeSystemMember>();


            if (ExtendSchemaIn != null)
            {
                dynamicTypes.Add(new QueryNameExtension(ExtendSchemaIn.QueryName));
            }

            return dynamicTypes;
        }

        public class QueryNameExtension : ObjectTypeExtension
        {
            private string fieldName;

            public QueryNameExtension(string fieldName)
            {
                this.fieldName = fieldName;
            }

            protected override void Configure(IObjectTypeDescriptor descriptor)
            {
                descriptor.Name(nameof(Queries));
                descriptor.Field(fieldName.FirstCharToLowerCase()).
                    Type(Syntax.ParseTypeReference("[UserInfo]!")).
                    Resolve(context => new ValueTask<IQueryable<UserInfo>>(new List<UserInfo>().AsQueryable())).
                    UseSorting(null);

                base.Configure(descriptor);
            }
        }
    }
}
