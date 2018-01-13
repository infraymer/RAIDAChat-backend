**RAIDAChat-backend**
---
Data transfer will be carried out through **_WebSocket._** The following **_template_** of a line for data transfer on the _server_ is used:
>```JSON
>{
>  "executeFunction": "fun_name",
>  "data": {
>     "Certain property set":""
>     }
>}

For transmission from the server on the _client_ to use such template:
>```JSON
>{
>  "callFunction": "fun_name_executed",
>  "success": "true_or_false",
>  "msgError": "Exception_text",  
>  "data": {
>     "Certain property set":""
>     }
>}
---

## API:

**_Description:_** Registration of the new user.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "registration",
>  "data": {
>     "login": "String",
>     "an": "GIUD",
>     "publicId": "GUID"
>     } 
>}
**_Return from Chat service:_**

>```
>"data": { }
**_Possible response errors:_**

1. The user with such login already exists
---

**_Description:_** Authorization of the user in a chat.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "authorization",
>  "data": {
>     "login": "String",
>     "an": "GUID"
>     } 
>}
**_Return from Chat service:_**

>```
>"data": {
>     "publicId": "GUID"
>}
**_Possible response errors:_**

1. Invalid login or AN.
---

**_Description:_** Creation of new group.\
**_Request to Chat service:_**
>```JSON
>{
>  "execFun": "createGroup",
>  "data": {
>     "groupName": "String",
>     "public_id": " GUID"
>     }
>}

**_Return from Chat service:_**
>```
>"data": {
>    "id": "GUID",
>    "name": "String" 
>}
**_Possible response errors:_**

---
**_Description:_** Adding of the user to group. \
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "addMemberInGroup",
>  "data": {
>     "memberLogin": "String",     
>     "groupId": "GUID"
>     }
>}
**_Return from Chat service:_**
>```
>For owner:
>"data": { }
>For other users:
>"data": {
>    "callFunction": "String",
>    "id": "GUID",
>    "name": "String" 
>}
**_Possible response errors:_**

1. User is not found
1. Group is not found
1. The user already consists in this group 
---

**_Description:_** Sending message for the server.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "sendMsg",
>  "data": {
>     "recipientId": "GUID",   
>     "recipientLogin": "String",
>     "toGroup": "Boolean",
>     "textMsg": "String",
>     "guidMsg": "GUID",
>     "sendTime": "Long",
>     "curFrg": "Integer",
>     "totalFrg": "Integer"
>     }
>}
**_Properties:_**
- *recipientId* - Id пользователя или группы, смотря для кого предназначено сообщение
- *recipientLogin* - Логин пользователя кому предназначено сообщение (Для одно человека сверяется recipientId или recipientLogin, для группы только recipientId)
- *toGroup* - Если сообщение предназначено для группы указываем **true**, если для определённого пользователя, то **false**
- *textMsg* - Непосредственно текст сообщения
- *guidMsg* - Идентификатор сообщения
- *sendTime* - Время отправки сообщения, в миллисекундах 
- *curFrg* - Номер части сообщения
- *totalFrg* - Общее число частей на которое было разбито сообщение
>
**_Return from Chat service:_**
>```
>"data": {
>    "guidMsg": "GUID",
>    "textMsg": "String",
>    "sender": "GUID",
>    "group": "Boolean",
>    "recipient": "GUID",
>    "sendTime": "Long",
>    "curFrg": "Integer",
>    "totalFrg": "Integer",
>    "senderName": "String",
>    "groupName": "String"
>}
**_Possible response errors:_**
>
1. User is not found
1. Group is not found

---
**_Description:_** Request for obtaining all messages from the server.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "getMsg",
>  "data": {
>     "getAll": "Boolean",     
>     "onGroup": "Boolean",
>     "onlyId": "GUID"
>     }
>}
**_Properties:_**
- *getAll* - Получить все имеющиеся на сервере сообщения для пользователя (**true**), если необходимо получить только от конкретного пользователя или группы, то указывается **false**. Если указано *true*, то поля *onGroup* и *onlyId* при обработке на сервере игнорируются.
- *toGroup* - Если необходимо получить сообщения группы указываем **true**, если от определённого пользователя, то **false**
- *onlyId* - Id соответствующей группы или пользователя

**_Return from Chat service:_**
>```
>"data": {
>    "guidMsg": "GUID",
>    "textMsg": "String",
>    "sender": "GUID",
>    "group": "Boolean",
>    "recipient": "GUID",
>    "sendTime": "Long",
>    "curFrg": "Integer",
>    "totalFrg": "Integer",
>    "senderName": "String",
>    "groupName": "String"
>}
**_Possible response errors:_**
1. User is not found
1. Group is not found

---
**_Description:_** Request for obtaining user he all groups from the server.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "getMyDialogs",
>  "data": {}
>}

**_Return from Chat service:_**
>```
>"data": {
>    "id": "GUID",
>    "name": "String",
>    "group": "Boolean"
>}

---
##EXAMPLES

Sample Request to Chat service for registration new user
>```JSON
>{
>  "execFun": "registration",
>  "data": {
>     "login": "myLogin",
>     "an": "99ab7bf3bef54f77b35ce8b5ee8f8260",
>     "publicId": "50b411ceab24adeb0539de62100646c0"
>     } 
>}
Sample Return from Chat service is success
>```JSON
>{
>  "callFunction":"registration",
>  "success":true,
>  "msgError":"",
>  "data":{}
>}
Sample Return from Chat service is error if user login already exists
>```JSON
>{
>  "callFunction":"registration",
>  "success":false,
>  "msgError":"This login already exists",
>  "data":{}
>}

Sample Request to Chat service for get all user messages
>```JSON
>{
>  "execFun": "getMsg",
>  "data": {
>     "getAll": "true",     
>     "onGroup": "false",
>     "onlyId": "00000000000000000000000000000000"
>  }
>}
Sample Return from Chat service is success
Sample Request to Chat service for get all user messages
>```JSON
>{
>  "callFunction": "getMsg",
>  "success": true,
>  "msgError": "",
>  "data":[{
>       "guidMsg": "a94cac44-561a-ce4f-dd34-042cb455cfb4",
>       "textMsg": "MTIz",
>       "sender": "80f7efc0-32dd-4a7c-97f6-9fca51ad3000",
>       "group": "False",
>       "recipient": "50b411ce-ab24-adeb-0539-de62100646c0",
>       "curFrg": 1,
>       "totalFrg": 1,
>       "sendTime": 1512757166456,
>       "senderName": "TestSenderLogin",
>       "groupName": "myLogin"
>     },
>     {
>       "guidMsg": "3ab190cf-0b94-1a83-b743-06b57523ca56",
>       "textMsg": "ZWZ3ZQ==",
>       "sender": "50b411ce-ab24-adeb-0539-de62100646c0",
>       "group": "True",
>       "recipient": "48A0CA06-57DE-4FB0-9CDC-86008B2A8EBE",
>       "curFrg": 1,
>       "totalFrg": 1,
>       "sendTime": 1512407677017,
>       "senderName": "myLogin",
>       "groupName": "ShareChat"
>  }]
>}

####Possible errors for all request
Sample Return from Chat service is error if parameters in "data" have invalid data-type
>```JSON
>{
>  "callFunction":"registration",
>  "success":false,
>  "msgError":"Invalid sending data",
>  "data":{}
>}
Sample Return from Chat service is error if user in not authorized
>```JSON
>{
>  "callFunction":"getMsg",
>  "success":false,
>  "msgError":"You are not authorized. To continue working you need to login.",
>  "data":{}
>}
Sample Return from Chat service is error if attempt was to call a non-existent function
>```JSON
>{
>  "callFunction":"getMsgError",
>  "success":false,
>  "msgError":"Function: 'getMsgError' not found",
>  "data":{}
>}
---
Operating procedure with the server:

1. To be connected to a websocket
1. To register (If for the first time)
1. Get authorization
1. Sending any command of the appropriate format