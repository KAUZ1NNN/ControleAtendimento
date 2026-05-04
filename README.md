# Controle de Atendimentos

Sistema para gerenciamento de chamados de suporte.

## Tecnologias
- C# (.NET 8)
- PostgreSQL
- HTML, CSS, JavaScript

## Como rodar o projeto

### Banco de Dados
1. Criar banco no PostgreSQL
2. Executar script em /database/script.sql

### Backend
dotnet run

### Frontend
cd frontend
npx serve

## Funcionalidades
- Cadastro de setores
- Cadastro de prioridades
- Abertura de chamados
- Início e finalização de atendimento
- Cálculo de tempo de atendimento
- Destaque de chamados em atraso
