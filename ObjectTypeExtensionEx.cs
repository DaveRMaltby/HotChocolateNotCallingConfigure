using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using System;

namespace HotChocolateNotCallingConfigure
{
    class ObjectTypeExtensionEx : ObjectTypeExtension
    {
        public ObjectTypeExtensionEx()
            : base()
        {

        }

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            //NOTE:  The this method never gets called!!
            Console.WriteLine("ISSUE SOLVED: ObjectTypeExtensionEx.Configure() was called.");

            var fieldDescriptor = descriptor as ObjectFieldDescriptor;
            fieldDescriptor.UseSorting(null);
            base.Configure(descriptor);
        }

        public static ObjectTypeExtensionEx CreateUnsafeEx(ObjectTypeDefinition definition)
            => new() { Definition = definition };
    }
}
