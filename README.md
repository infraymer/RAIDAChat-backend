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
>     "login": "GUID - login",
>     "an": "GIUD - password"
>     } 
>}
**_Return from Chat service:_**

>```
>"data": {
>    Nothing 
>}
**_Possible response errors:_**

1. The user with such login already exists
---

**_Description:_** Authorization of the user in a chat.\
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "authorization",
>  "data": {
>     "login": "GUID - login",
>     "an": "GUID - password"
>     } 
>}
**_Return from Chat service:_**

>```
>"data": {
>    Nothing
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
>     "groupName": "name_for_new_group",
>     "public_id": " GUID - group"
>     }
>}

**_Return from Chat service:_**
>```
>"data": {
>    "id": "Public id group",
>    "name": "Name of group" 
>}
**_Possible response errors:_**

---
**_Description:_** Adding of the user to group. \
**_Request to Chat service:_**

>```JSON
>{
>  "execFun": "addMemberInGroup",
>  "data": {
>     "memberId": "GUID - user_login",     
>     "groupId": "GUID - group_id"
>     }
>}
**_Return from Chat service:_**
>```
>For owner:
>"data": {
>    Nothing 
>}
>For other users:
>"data": {
>    "callFunction": "addMemberInGroup",
>    "id": "Public id group",
>    "name": "Name of group" 
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
>     "recipientId": " GUID - login_of_member_or_groupId",     
>     "toGroup": "true_or_false",
>     "textMsg": "send_message",
>     "guidMsg": "GUID - guid_message",
>     "sendTime": "message_sending_time",
>     "curFrg": "current_fragment_msg",
>     "totalFrg": "all_fragments_in_msg"
>     }
>}
**_Properties:_**
- *recipientId* - Id пользователя или группы, смотря для кого предназначено сообщение
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
>    "guidMsg": "guid_message",
>    "textMsg": "message",
>    "sender": "sender_id(login)_or_groupId",
>    "group": "true_of_false",
>    "recipient": "Recipient public id",
>    "sendTime": "message_sending_time",
>    "curFrg": "current_fragment_msg",
>    "totalFrg": "all_fragments_in_msg",
>    "groupName": "Name of group. If it's group"
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
>     "getAll": "true_or_false",     
>     "onGroup": "true_or_false",
>     "onlyId": "GUID - sender_id"
>     }
>}
**_Properties:_**
- *getAll* - Получить все имеющиеся на сервере сообщения для пользователя (**true**), если необходимо получить только от конкретного пользователя или группы, то указывается **false**. Если указано *true*, то поля *onGroup* и *onlyId* при обработке на сервере игнорируются.
- *toGroup* - Если необходимо получить сообщения группы указываем **true**, если от определённого пользователя, то **false**
- *onlyId* - Id соответствующей группы или пользователя

**_Return from Chat service:_**
>```
>"data": {
>    "guidMsg": "guid_message",
>    "textMsg": "message",
>    "sender": "sender_id(login)_or_groupId",
>    "group": "true_of_false",
>    "recipient": "Recipient public id",
>    "sendTime": "message_sending_time",
>    "curFrg": "current_fragment_msg",
>    "totalFrg": "all_fragments_in_msg",
>    "groupName": "Name of group. If it's group"
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
>    "id": "public_group_id_or_member_id - GUID",
>    "name": "group_name_part",
>    "group": "Is this a group? (true_or_false)"
>}

---
Operating procedure with the server:

1. To be connected to a websocket
1. To register (If for the first time)
1. Get authorization
1. Sending any command of the appropriate format