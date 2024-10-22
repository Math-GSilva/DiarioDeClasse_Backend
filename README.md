# DiarioDeClasse Backend

## Visão Geral
Este repositório contém o backend para a aplicação DiarioDeClasse, que gerencia a autenticação e operações relacionadas ao sistema de usuários utilizando JWT para geração e validação de tokens. O backend é desenvolvido em C# com a plataforma .NET.

## Estrutura do Projeto
A solução está organizada nos seguintes módulos:

- **DiarioDeClasse.API**: Contém a API principal do sistema, responsável por expor endpoints e controlar as rotas HTTP.
- **DiarioDeClasse.Domain**: Inclui a lógica de domínio, como entidades, DTOs e interfaces de repositórios.
- **DiarioDeClasse.Infra**: Contém a configuração de infraestrutura, como a configuração do banco de dados e repositórios concretos.
- **DiarioDeClasse.Repository**: Implementação dos repositórios que interagem diretamente com o banco de dados.
- **DiarioDeClasse.Test**: Testes unitários do projeto, garantindo a qualidade e correção das funcionalidades implementadas.

## Pré-requisitos
Antes de executar o projeto, você precisa garantir que os seguintes componentes estejam instalados:

- .NET SDK 8.0
- Banco de dados SQL Server (ou outro configurado no projeto)
- Ferramenta para gerenciamento de pacotes (NuGet)

## Instalação
Clone o repositório:

```bash
git clone https://github.com/seu-usuario/DiarioDeClasse_Backend.git
cd DiarioDeClasse_Backend
```

Restaure as dependências do projeto:

```bash
dotnet restore
```

Configure a string de conexão com o banco de dados em `appsettings.json` dentro da pasta `DiarioDeClasse.API`.

Execute o projeto:

```bash
dotnet run --project DiarioDeClasse.API
```

## Arquitetura
O projeto segue uma arquitetura limpa, separando a lógica de domínio da infraestrutura e da camada de API. A comunicação com o banco de dados é feita através de repositórios, e a autenticação é gerenciada pelo AuthService utilizando JWT.

## Principais Componentes
- **Autenticação**: O serviço `AuthService` gerencia a autenticação de usuários, validação de senhas (com BCrypt) e geração de tokens JWT.
- **JWT**: Tokens JWT são utilizados para autenticar usuários e garantir a segurança das requisições.
- **Testes Unitários**: O projeto utiliza xUnit e Moq para realizar testes unitários, com cobertura para o `AuthService`.

## Testes
Os testes unitários estão localizados na pasta `DiarioDeClasse.Test`. Para rodar os testes, utilize o comando:

```bash
dotnet test
```

Os testes cobrem os principais serviços do projeto, incluindo autenticação e validação de tokens JWT.

## Configurações
### Arquivo `appsettings.json`
- **Jwt**: Chave usada para assinar os tokens JWT.
- **ConnectionStrings**: String de conexão para o banco de dados.

Exemplo de configuração:

```bash
{
  "Jwt": {
    "Key": "sua-chave-secreta"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=DiarioDeClasseDb;Trusted_Connection=True;"
  }
}
```

## Contribuição
Matheus Gabriel da Silva; Larissa Hoffmann de Souza; Eduardo da Maia Haak; Lukas Thiago Rodrigues; Mateus Akira de Oliveira
