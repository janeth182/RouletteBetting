# RouletteBetting

Metodo Get

http://www.jvargas.somee.com/api/Roulette/ListRoulettes

Metodo Post 

http://www.jvargas.somee.com/api/Roulette/AddRoulette

JSON modelo a enviar por metodo POST

{
  "Description" : "Roulette 5"
}

http://www.jvargas.somee.com/api/Roulette/OpenRoulette

JSON modelo a enviar por metodo POST

{
  "IdRoulette" : 5
}

http://www.jvargas.somee.com/api/Roulette/ToBet

JSON modelo a enviar por metodo POST

{
  "IdRoulette" : 5,
  "amount": 500,
  "number": 15,
  "color": ""
}

En Headers enviar el campo 

kEY       VALUE
IdClient  1

http://www.jvargas.somee.com/api/Roulette/CloseRoulette

JSON modelo a enviar por metodo POST

{
  "IdRoulette" : 5
}

