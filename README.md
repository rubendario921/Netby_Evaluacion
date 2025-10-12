# Netby - Evaluacion
EVALUACIÓN TÉCNICA DE CONOCIMIENTOS FULLSTACK .NET Y REACT O ANGULAR

#Estructura
MiProyecto/
│
├── src/
│   ├── MiProyecto.Domain/                    # CAPA DE DOMINIO (Núcleo)
│   │   ├── Entities/                         # Entidades de negocio
│   │   ├── ValueObjects/                     # Objetos de valor
│   │   ├── Aggregates/                       # Agregados
│   │   ├── Interfaces/                       # Interfaces del dominio
│   │   ├── Exceptions/                       # Excepciones de dominio
│   │   └── Events/                           # Eventos de dominio
│   │
│   ├── MiProyecto.Application/               # CAPA DE APLICACIÓN (Casos de Uso)
│   │   ├── UseCases/                         # Casos de uso
│   │   │   ├── Products/
│   │   │   │   ├── Create/
│   │   │   │   │   ├── CreateProductCommand.cs
│   │   │   │   │   ├── CreateProductHandler.cs
│   │   │   │   │   └── CreateProductValidator.cs
│   │   │   │   ├── GetById/
│   │   │   │   └── GetAll/
│   │   ├── DTOs/                             # Data Transfer Objects
│   │   ├── Ports/                            # PUERTOS (Interfaces)
│   │   │   ├── Input/                        # Puertos de entrada
│   │   │   └── Output/                       # Puertos de salida (Repositorios, etc)
│   │   ├── Mappers/                          # Mappers entre capas
│   │   └── Common/                           # Clases comunes
│   │
│   ├── MiProyecto.Infrastructure/            # CAPA DE INFRAESTRUCTURA (Adaptadores)
│   │   ├── Persistence/                      # Adaptador de Persistencia
│   │   │   ├── Context/
│   │   │   │   └── ApplicationDbContext.cs
│   │   │   ├── Repositories/                 # Implementación de repositorios
│   │   │   ├── Configurations/               # Configuraciones EF Core
│   │   │   └── Migrations/
│   │   ├── ExternalServices/                 # Servicios externos
│   │   │   ├── Email/
│   │   │   └── Payment/
│   │   └── DependencyInjection.cs            # Registro de dependencias
│   │
│   └── MiProyecto.API/                       # CAPA DE PRESENTACIÓN (Adaptador REST)
│       ├── Controllers/                      # Controladores REST
│       ├── Middlewares/                      # Middlewares personalizados
│       ├── Filters/                          # Filtros
│       ├── Extensions/                       # Extensiones
│       ├── Program.cs                        # Punto de entrada
│       └── appsettings.json
│
└── tests/
    ├── MiProyecto.Domain.Tests/              # Tests unitarios del dominio
    ├── MiProyecto.Application.Tests/         # Tests de casos de uso
    ├── MiProyecto.Infrastructure.Tests/      # Tests de infraestructura
    └── MiProyecto.API.Tests/                 # Tests de integración

