# 🧪 Casos de Teste - Gadeia's Bar

Repositório contendo a especificação detalhada e a suíte de **Casos de Teste** do sistema de gerenciamento de restaurantes e bares **Gadeia's Bar**.

---

## 📌 Visão Geral do Projeto

O **Gadeia's Bar** é um sistema de gestão voltado para otimizar o atendimento, controle de mesas, comandas, garçons e pedidos. Esta suíte de testes foi elaborada no padrão corporativo da empresa, garantindo cobertura total dos requisitos funcionais, regras de negócio e cenários de exceção.

---

## 📊 Cobertura de Testes

A suíte possui **44 casos de teste** distribuídos em 5 módulos principais:

| Módulo | Total de Casos | Positivos | Negativos | Principais Focos de Validação |
| :--- | :---: | :---: | :---: | :--- |
| **Produtos** | **13** | 6 | 7 | Nomes únicos, tamanho do nome (2–100 chars), preço não-negativo, CRUD. |
| **Mesa** | **7** | 5 | 2 | Número de mesa único, quantidade de lugares positiva, controle de estado (Livre/Ocupada). |
| **Garçom** | **12** | 4 | 8 | Nome único, CPF único/válido, telefone único no formato `(DD) XXXXX-XXXX`, ID de conta único. |
| **Conta** | **10** | 7 | 3 | Ocupação automática da mesa ao abrir, liberação ao fechar, cálculo de saldo, bloqueio de itens pós-fechamento. |
| **Pedido** | **2** | 1 | 1 | Associação obrigatória de pedidos/itens a uma conta aberta específica. |
| **TOTAL** | **44** | **23** | **21** | **Cobertura 100% dos Requisitos Funcionais** |

---

## 🏢 Estrutura da Planilha (`.xlsx`)

A planilha `Casos_de_Teste_Gadeias_Bar.xlsx` segue a arquitetura padrão dividida em duas abas principais:

### 1. Aba: `Casos de Teste`
Contém a matriz completa de testes com as seguintes colunas de rastreabilidade:
- **Dev**: Identificação do desenvolvedor responsável.
- **ID**: Código único do teste (ex: `CT-PROD-001`, `CT-MESA-001`, `CT-GARC-001`, `CT-CONT-001`).
- **Módulo**: Sistema ou domínio testado (*Produtos*, *Mesa*, *Garçom*, *Conta*, *Pedido*).
- **Funcionalidade**: Ação específica (*Cadastro*, *Edição*, *Exclusão*, *Visualização*, *Fechamento*, etc.).
- **Caso de Teste**: Descrição objetiva do cenário.
- **Tipo**: `Positivo` (Caminho feliz) ou `Negativo` (Tratamento de erro/exceção).
- **Pré-condições**: Estado necessário do sistema antes da execução.
- **Resultado Esperado**: Comportamento esperado da aplicação.
- **Status**: Estado de execução (`Backlog`, `Pronto`, `Em Andamento`).
- **Matriz de Rastreabilidade**:
  - `Regras de Domínio`
  - `Casos de Uso`
  - `Persistência/Infra`
  - `Jornada do Usuário`

### 2. Aba: `Resumo`
Dashboard sintético para acompanhamento dos indicadores da suíte de teste.

---

## 🛠️ Regras de Negócio Testadas

### 🍺 Módulo Produtos
- [x] Nome único obrigatório.
- [x] Nome deve ter entre 2 e 100 caracteres.
- [x] Valor não pode ser negativo ($ \ge 0 $).
- [x] Categoria de produto (Comida / Bebida).

### 🪑 Módulo Mesa
- [x] Número da mesa não pode ser repetido.
- [x] Quantidade de lugares deve ser estritamente positiva ($ > 0 $).
- [x] Alteração automática de status: **Ocupada** (ao abrir conta) / **Disponível** (ao fechar conta).

### 👔 Módulo Garçom
- [x] Nome único (2 a 100 caracteres).
- [x] CPF com máscara/tamanho válido e sem duplicidade.
- [x] Telefone no formato correto `(DD) XXXXX-XXXX` e sem duplicidade.
- [x] Garantia de unicidade no ID da conta associada.

### 📝 Módulo Conta & Pedidos
- [x] Atualização dinâmica do valor final da conta ao adicionar/remover produtos.
- [x] Bloqueio imediato de inclusão de novos produtos após fechamento da conta.
- [x] Vinculação obrigatória de cada pedido a uma conta válida e aberta.
