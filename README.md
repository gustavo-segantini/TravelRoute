# Rota de Viagem

## Descrição
Um turista deseja viajar pelo mundo pagando o menor preço possível, independentemente do número de conexões necessárias. Este programa facilita ao turista escolher a melhor rota para sua viagem.

## Tecnologias Utilizadas
- C# .NET Core 8
- Docker

## Funcionalidades
- Inserção de rotas através de um arquivo de entrada.
- Consulta da melhor rota e seu respectivo valor através de uma interface de console.
- Registro e consulta de rotas através de endpoints REST.

## Exemplo de Entrada
O arquivo de entrada deve conter as rotas no seguinte formato:


GRU,BRC,10\
BRC,SCL,5\
GRU,CDG,75\
GRU,SCL,20\
GRU,ORL,56\
ORL,CDG,5\
SCL,ORL,20\
ORL,GRU,0


## Explicação
Caso desejemos viajar de GRU para CDG, existem as seguintes rotas:
- GRU - BRC - SCL - ORL - CDG ao custo de $40
- GRU - ORL - CDG ao custo de $64
- GRU - CDG ao custo de $75
- GRU - SCL - ORL - CDG ao custo de $45

O melhor preço é da rota 1, logo, o output da consulta deve ser:

GRU - BRC - SCL - ORL - CDG -> $40

## Execução do Programa
A inicialização do teste se dará por linha de comando onde o primeiro argumento é o arquivo com a lista de rotas inicial.

## Interfaces de Consulta

### Interface de Console
A interface de console deverá receber um input com a rota no formato "DE-PARA" e imprimir a melhor rota e seu respectivo valor.


please enter the route: GRU-CDG best route: GRU - BRC - SCL - ORL - CDG > $40

please enter the route: BRC-CDG best route: BRC - ORL > $30


### Interface REST
A interface REST deverá suportar:
- Registro de novas rotas. Essas novas rotas devem ser persistidas no arquivo CSV utilizado como input (`input-routes.csv`).
- Consulta da melhor rota entre dois pontos.

#### Endpoints
- **POST /routes**: Registra uma nova rota.
```bash
curl --location 'https://localhost:32773/routes' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--data '{
  "from": "ORL",
  "to": "GRU",
  "cost": 0
}'
```

- **GET /routes/best**: Consulta a melhor rota entre dois pontos.
```bash
- curl --location 'http://localhost:8080/routes/best?from=GRU&to=BRC' \
--header 'accept: application/json'
```

## Como Executar
1. Clone o repositório.
2. Navegue até o diretório do projeto.
3. Construa a imagem Docker e execute o container Docker:

```bash
docker compose up --build
```

## Licença
Este projeto está licenciado sob a Licença GNU GENERAL PUBLIC LICENSE - veja o arquivo LICENSE para mais detalhes.

