const express = require("express");
   
const app = express();
   
// создаем парсер для данных в формате json
const jsonParser = express.json();
 
// настройка CORS
app.use(function(req, res, next) {
   res.header("Access-Control-Allow-Origin", "*");
   res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
   res.header("Access-Control-Allow-Methods", "GET, PATCH, PUT, POST, DELETE, OPTIONS");
   next();  // передаем обработку запроса методу app.post("/postuser"...
 });
  
// обработчик по маршруту localhost:3000/postuser
app.post("/postuser", jsonParser, function (request, response) {
 
    // если не переданы данные, возвращаем ошибку
    if(!request.body) return response.sendStatus(400);
     
    // получаем данные
    let username = request.body.name;
    let userage = request.body.age;
    // имитируем некоторую обработку данных, например, изменим значение userage
    userage = userage + 10;
     
    // отправка данных обратно клиенту
    response.json({"name": username, "age": userage});
});
  
app.listen(3000);