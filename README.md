# Fase 4 - Projeto de Criação de Contatos - Arquitetura de Microserviços

Este projeto tem como objetivo implementar uma aplicação para gerenciamento de contatos utilizando uma arquitetura baseada em microserviços. A solução foi dividida em dois serviços independentes, um *producer* e um *consumer*, ambos desenvolvidos com .NET 8. O *producer* é uma Web API, enquanto o *consumer* é um Worker Service. A comunicação entre esses dois serviços é feita através do RabbitMQ, com o *consumer* sendo responsável por processar mensagens da fila e interagir com o banco de dados.

## Arquitetura
![Arquitetura fase 4](https://github.com/user-attachments/assets/b7bf729f-563d-4049-afc4-536e901e41ef)

A arquitetura do projeto é composta pelos seguintes elementos:

1. **Producer (Web API)**: Expõe endpoints para a criação, edição e exclusão de contatos. Essas requisições são recebidas pela API e enviadas para a fila do RabbitMQ, onde o *consumer* irá processá-las.

2. **Consumer (Worker Service)**: Responsável por ler as mensagens da fila RabbitMQ, identificar a operação desejada (via *enum*) e executar a operação correspondente no banco de dados SQL.

3. **RabbitMQ**: Utilizado como sistema de mensagens para comunicação assíncrona entre os serviços. Possui uma única fila chamada "contato".

4. **Dead Letter Queue (DLQ)**: Mensagens que não podem ser processadas são encaminhadas automaticamente para uma fila de erros (DLQ), para posterior tratamento por Azure Functions.

5. **Serviço de Kubernetes da Azure(AKS)**: 
   - Utilizamos o AKS para hospedar nossa aplicação em um ambiente orquestrado e escalável. Foram criados 4 pods para os principais componentes: Producer, Consumer, Prometheus e Grafana.
   - O processo de implantação foi facilitado pelo uso de arquivos de configuração Kubernetes, incluindo Deployments, Services e ConfigMaps, permitindo uma automação eficiente e padronizada do ambiente.
   - O Producer e o Grafana foram expostos externamente, possibilitando o consumo da API via navegador ou ferramentas como Postman, além do acesso a dashboards interativos para monitoramento em tempo real da aplicação.

6. **Monitoramento com Prometheus e Grafana**: Métricas de desempenho dos serviços são coletadas pelo Prometheus e visualizadas através do Grafana, ambos implantados como serviços em pods no cluster.

## Novidades nesta fase (Kubernetes e AKS)

Para melhorar a escalabilidade, gerenciamento e monitoramento da aplicação, foram feitas as seguintes implementações usando Kubernetes (K8s):

- **Deploy em Kubernetes (K8s)**: Toda a aplicação foi conteinerizada e implantada em um cluster Kubernetes.
- **AKS (Azure Kubernetes Service)**: O ambiente de produção está hospedado em um cluster gerenciado no Azure (AKS).
- **Pods com ReplicaSets**: Tanto o *producer* quanto o *consumer* rodam em pods gerenciados por ReplicaSets, permitindo alta disponibilidade e escalabilidade automática.
- **Volumes Persistentes**: Volumes foram utilizados para garantir persistência de dados e logs mesmo após reinicializações dos pods.
- **ConfigMaps e Secrets**: Configurações sensíveis e variáveis de ambiente foram gerenciadas de forma segura via ConfigMaps e Secrets no cluster.
- **Serviços (Services)**: Serviços do tipo ClusterIP e LoadBalancer foram criados para expor os pods internamente e externamente no cluster.

Além disso, os seguintes serviços também foram incluídos no cluster Kubernetes:

- **Prometheus e Grafana**: Implantados como pods para coleta e visualização de métricas do sistema, proporcionando monitoramento completo do ambiente.

## Tecnologias Utilizadas

- **.NET 8**: Desenvolvimento do *producer* (Web API) e *consumer* (Worker Service).
- **RabbitMQ**: Sistema de mensageria assíncrono.
- **SQL Database**: Armazenamento relacional de contatos.
- **Azure Functions**: Processamento assíncrono de erros (DLQ) e leitura de dados.
- **Azure API Management (APIM)**: Gerenciamento de APIs com *rate limit*.
- **Kubernetes (K8s)**: Orquestração de containers.
- **Azure Kubernetes Service (AKS)**: Cluster Kubernetes gerenciado na nuvem.
- **Prometheus**: Coleta de métricas.
- **Grafana**: Visualização de métricas e dashboards.
- **Docker**: Criação de imagens dos microserviços.
- **Azure Container Registry (ACR)**: Repositório de imagens Docker.

## Como Funciona

1. O Azure API Management (APIM) realiza chamadas controladas ao *producer*, respeitando o limite de requisições configurado.
2. O *producer* recebe requisições para criar, editar ou deletar contatos.
3. A Web API envia uma mensagem para a fila "contato" do RabbitMQ.
4. O *consumer* lê a mensagem, interpreta o *enum* de operação, e realiza a ação no banco de dados SQL.
5. Caso ocorra erro, a mensagem é encaminhada para a DLQ.
6. Prometheus coleta métricas dos serviços (como uso de CPU, memória, erros, etc.).
7. O Grafana exibe essas métricas em tempo real em painéis de monitoramento.

## Fluxo de Mensagens

- **Fila RabbitMQ**: Centraliza todas as mensagens relacionadas a contatos.
- **Enum de Operações**: Controla o tipo de operação executada pelo *consumer* (Inserir, Editar, Deletar).
- **DLQ**: Mensagens com falha de processamento são encaminhadas para a fila de erro.
