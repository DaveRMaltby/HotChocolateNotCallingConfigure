using System;

namespace HotChocolateNotCallingConfigure
{
    public class Mutations
    {
        private readonly CustomTypeModule typeModule;

        public Mutations(CustomTypeModule typeModule) =>
            this.typeModule = typeModule ?? throw new ArgumentNullException(nameof(typeModule));

        public ExtendSchemaOut ExtendSchema(ExtendSchemaIn schemaDescription)
        {
            var results = typeModule.UpdateSchema(schemaDescription);
            return new ExtendSchemaOut { Success = results };
        }

    }
}
