using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                //Extend Queries with another field.
                var typeDefinition = new ObjectTypeDefinition(nameof(Queries));

                var fieldDefinition = new ObjectFieldDefinition(ExtendSchemaIn.QueryName.FirstCharToLowerCase(),
                                              type: TypeReference.Parse("[UserInfo]!"),
                                              pureResolver: ctx => new List<UserInfo>() );

                typeDefinition.Fields.Add(fieldDefinition);

                //NOTE:  Calling a derived class of ObjectTypeExtension to inject the UseSorting facility
                var objectTypeExtension = ObjectTypeExtensionEx.CreateUnsafeEx(typeDefinition);
                dynamicTypes.Add(objectTypeExtension);
            }

            return dynamicTypes;
        }
    }
}
