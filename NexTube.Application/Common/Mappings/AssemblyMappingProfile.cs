using AutoMapper;
using System.Reflection;

namespace NexTube.Application.Common.Mappings {
    public class AssemblyMappingProfile : Profile {
        public AssemblyMappingProfile(Assembly assembly) => ApplyMappingsFromAssembly(assembly);

        private void ApplyMappingsFromAssembly(Assembly assembly) {
            // отримати список усіх классів, які реалізують інтерфейс IMapWith<>
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach ( var type in types ) {
                // код нижче створює об'єкти по кожному типу, отримує методи Mapping з цих об'єктів
                // і викликає цей метод, передавши у нього масив об'єктів із єдиним елементом - посилання на 
                // об'єкт цього класу AssemblyMappingProfile

                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }

        }
    }
}
