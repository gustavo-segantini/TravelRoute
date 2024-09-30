# Rota de Viagem

## Descri��o
Um turista deseja viajar pelo mundo pagando o menor pre�o poss�vel, independentemente do n�mero de conex�es necess�rias. Este programa facilita ao turista escolher a melhor rota para sua viagem.

## Tecnologias Utilizadas
- C# .NET Core 8
- Docker

## Funcionalidades
- Inser��o de rotas atrav�s de um arquivo de entrada.
- Consulta da melhor rota e seu respectivo valor atrav�s de uma interface de console.
- Registro e consulta de rotas atrav�s de endpoints REST.

## Exemplo de Entrada
O arquivo de entrada deve conter as rotas no seguinte formato:

GRU,BRC,10 BRC,SCL,5 GRU,CDG,75 GRU,SCL,20 GRU,ORL,56 ORL,CDG,5 SCL,ORL,20


## Explica��o
Caso desejemos viajar de GRU para CDG, existem as seguintes rotas:
- GRU - BRC - SCL - ORL - CDG ao custo de $40
- GRU - ORL - CDG ao custo de $64
- GRU - CDG ao custo de $75
- GRU - SCL - ORL - CDG ao custo de $45

O melhor pre�o � da rota 1, logo, o output da consulta deve ser:

GRU - BRC - SCL - ORL - CDG -> $40


## Execu��o do Programa
A inicializa��o do teste se dar� por linha de comando onde o primeiro argumento � o arquivo com a lista de rotas inicial.


## Interfaces de Consulta

### Interface de Console
A interface de console dever� receber um input com a rota no formato "DE-PARA" e imprimir a melhor rota e seu respectivo valor.

please enter the route: GRU-CDG best route: GRU - BRC - SCL - ORL - CDG > $40

please enter the route: BRC-CDG best route: BRC - ORL > $30


### Interface REST
A interface REST dever� suportar:
- Registro de novas rotas. Essas novas rotas devem ser persistidas no arquivo CSV utilizado como input (`input-routes.csv`).
- Consulta da melhor rota entre dois pontos.

#### Endpoints
- **POST /routes**: Registra uma nova rota.
- **GET /routes/best**: Consulta a melhor rota entre dois pontos.

## Como Executar
1. Clone o reposit�rio.
2. Navegue at� o diret�rio do projeto.
3. Construa a imagem Docker:
4. Execute o container Docker:

docker compose up --build

## Contribui��o
Sinta-se � vontade para contribuir com melhorias. Fa�a um fork do projeto, crie uma branch para sua feature ou corre��o de bug, e envie um pull request.

## Licen�a
Este projeto est� licenciado sob a Licen�a GNU GENERAL PUBLIC LICENSE - veja o arquivo LICENSE para mais detalhes.

