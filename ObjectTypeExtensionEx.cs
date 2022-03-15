using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;

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

            var fieldDescriptor = descriptor as ObjectFieldDescriptor;
            fieldDescriptor.UseSorting(null);
            base.Configure(descriptor);
        }

        public static ObjectTypeExtensionEx CreateUnsafe(ObjectTypeDefinition definition)
            => new() { Definition = definition };
    }
}
