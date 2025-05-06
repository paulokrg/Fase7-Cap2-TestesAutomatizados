# language: pt-BR
Funcionalidade: Verificar saúde da API
  Para garantir que o serviço esteja no ar

  Cenário: Health check retorna 200
    Dado que o serviço está rodando em "https://localhost:5000"
    Quando faço uma requisição GET no endpoint "/health"
    Então o código de resposta deve ser 200
    E o corpo da resposta deve conter "status": "UP"
    E a resposta deve obedecer ao schema "health-schema.json"
