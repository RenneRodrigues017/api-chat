# APIChat

APIChat é uma API ASP.NET (provavelmente .NET 9) para gerenciamento de chamados, usuários e interações com IA.

## Descrição
Projeto de exemplo para gerenciar chamados, histórico e interações IA. Contém controllers, models, contexto EF Core e migrations.

## Pré-requisitos
- .NET SDK 9 (ou versão compatível instalada)
- Git
- (Opcional) SQL Server ou outro provedor configurado em `appsettings.json`

## Configuração
1. Clone o repositório (se não estiver local):

   git clone https://github.com/RenneRodrigues017/api-chat.git
   cd api-chat

2. Ajuste `appsettings.json` (ou `appsettings.Development.json`) para apontar a string de conexão do seu banco de dados.

3. Aplicar migrações (EF Core):

   dotnet ef database update

   Observação: se o `dotnet-ef` não estiver instalado, instale globalmente com:

   dotnet tool install --global dotnet-ef

## Como rodar
- Build:

  dotnet build

- Rodar:

  dotnet run

A API ficará disponível em `https://localhost:5001` ou `http://localhost:5000` por padrão (conforme `launchSettings.json`).

## Endpoints (rápido)
- `GET /api/chamados` — listar chamados
- `POST /api/chamados` — criar chamado
- `GET /api/usuarios` — listar usuários

(Consulte os controllers em `Controller/` para lista completa e formatos de request/response.)

## Contribuição
- Abra issues para bugs ou melhorias.
- Crie branches por feature: `feature/nome-da-feature` e faça PR para `main`.

## Observações de segurança
- Não subir arquivos com segredos (`appsettings.Development.json` contém configurações locais); `.gitignore` já protege `appsettings.Development.json`.

## Contato
Renne Rodrigues
