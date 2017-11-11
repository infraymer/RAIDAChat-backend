using Newtonsoft.Json;
using RAIDAChat.Models;
using RAIDAChat.Reflection.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
 

namespace RAIDAChat.ReflectionClass
{
    public class MainAction
    {
        public AuthInfoWithSocket Auth(object data)
        {

            /*
             {
                "execFun": "subscribe",
                "data": {
                    "login": "80f7efc032dd4a7c97f69fca51ad3000",
                    "an": "d842703c8acb4bd893876d700f60683e"
                }
             }
             */

            AuthInfoWithSocket output = new AuthInfoWithSocket();

            AuthInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<AuthInfo>(data.ToString());

                using (var db = new CloudChatEntities())
                {
                    if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                    {
                        output.auth = true;
                        output.login = info.login;
                        output.an = info.an;
                    }
                    else
                    {
                        output.auth = false;
                    }
                }
            }
            catch
            {
                output.auth = false;
            }
            return output;
        }

        private OutputSocketMessageWithUsers registration(Object val)
        {
            /*
            {
                "execFun": "registration",
                "data": {
                    "login": "80f7efc032dd4a7c97f69fca51ad3000",
                    "an": "d842703c8acb4bd893876d700f60683e"
                }
            }

            {
                "execFun": "registration",
                "data": {
                    "login": "788FEFAD0ED24436AD73D968685110E8",
                    "an": "0AA700EEA5F44D64B9E0243EFB08C4DB"
                }
            }
            */

            OutputSocketMessage output = new OutputSocketMessage("registration", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            string vall = val.ToString();
            AuthInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<AuthInfo>(vall);
            }
            catch 
            {
                output.success = false;
                output.msgError = "Некорректый блок data";
                rez.msg = output;
                return rez;            
            }

           
            using (var db = new CloudChatEntities())
            {

                if(db.members.Any(it => it.public_id == info.login))
                {
                    output.success = false;
                    output.msgError = "Пользователь с таким логином уже существует";
                }
                else
                {
                    db.usp_membersInsert(info.login, info.an, Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), "", new byte[5], 0, "redy");
                }
            }

            rez.msg = output;
            rez.usersId.Add(info.login);

            return rez;
        }

        private OutputSocketMessageWithUsers creategroup(Object val)
        {
            /*
             *  public_id - Добавить в API (Id Groups)
                        {
                            "execFun": "createGroup",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "groupName": "MeCloseGroup",
                                "public_id": "48A0CA0657DE4FB09CDC86008B2A8EBE"
                            }
                        }
                        */

            OutputSocketMessage output = new OutputSocketMessage("createGroup", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            GroupInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<GroupInfo>(val.ToString());
            }
            catch
            {
                output.success = false;
                output.msgError = "Некорректый блок data";
                rez.msg = output;
                return rez;
            }

            using (var db = new CloudChatEntities())
            {

                if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                {
                    Guid owner = db.usp_membersSelect(info.login).First().private_id;

                    db.usp_groupsInsert(info.public_id, info.name, owner, "", Guid.Empty);
                    db.usp_group_membersInsert(info.public_id, owner, "allow_or_deny");
                }
                else
                {
                    output.success = false;
                    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                }
            }

            rez.msg = output;
            rez.usersId.Add(info.login);
            return rez;
        }

        private OutputSocketMessageWithUsers addmemberingroup(Object val)
        {
            /*
             
             * 
                        {
                            "execFun": "addMemberInGroup",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "memberId": "788FEFAD0ED24436AD73D968685110E8",
                                "groupId": "48A0CA0657DE4FB09CDC86008B2A8EBE"
                            }
                        }
                        */

            OutputSocketMessage output = new OutputSocketMessage("addMemberInGroup", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            AddMemberInGroupInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<AddMemberInGroupInfo>(val.ToString());
            }
            catch
            {
                output.success = false;
                output.msgError = "Некорректый блок data";
                rez.msg = output;
                return rez;
            }

            using (var db = new CloudChatEntities())
            {

                if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                {
                    Guid owner = db.members.FirstOrDefault(it => it.public_id == info.login && it.an == info.an).private_id;
                    if(db.members.Any(it => it.public_id == info.memberId))
                    {

                        if(db.group_members.Any(it=>it.member_id==owner && it.group_id==info.groupId))
                        {
                            Guid memberId = db.usp_membersSelect(info.memberId).First().private_id;
                            if (db.group_members.Any(it=> it.group_id==info.groupId && it.member_id == memberId))
                            {
                                /*Добавить в API*/
                                output.success = false;
                                output.msgError = "Пользователь уже состоит в данной группе";
                            }
                            else
                            {
                                db.usp_group_membersInsert(info.groupId, memberId, "I don't known");
                                rez.usersId.Add(info.memberId);
                            }
                        }
                        else
                        {
                            output.success = false;
                            output.msgError = "Группа не найдена";
                        }                    
                    }
                    else
                    {
                        output.success = false;
                        output.msgError = "Пользователь не найден";
                    }
                   
                }
                else
                {
                    output.success = false;
                    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                }
            }

            rez.msg = output;
            rez.usersId.Add(info.login);
            
            return rez;
        }

        private OutputSocketMessageWithUsers sendmsg(Object val)
        {
            /*
                        {
                            "execFun": "sendMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "recipientId": "788FEFAD0ED24436AD73D968685110E8",     
                                "toGroup": "false",
                                "textMsg": "test message for one user",
                                "guidMsg": "91D8333FA55B40AFB46CA63E214C93C8"
                            }
                        }

                        {
                            "execFun": "sendMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "recipientId": "48A0CA0657DE4FB09CDC86008B2A8EBE",     
                                "toGroup": "true",
                                "textMsg": "test message for group",
                                "guidMsg": "CA5BBD6B488941FEACFA19692D3087E0"
                            }
                        }
                        {
                            "execFun": "sendMsg",
                            "data": {
                                "login": "788FEFAD0ED24436AD73D968685110E8",
                                "an": "0AA700EEA5F44D64B9E0243EFB08C4DB",
                                "recipientId": "80f7efc032dd4a7c97f69fca51ad3000",     
                                "toGroup": "false",
                                "textMsg": "response fo test message",
                                "guidMsg": "19D2B66BB477467ABAE72AC79F5A2C48"
                            }
                        }
                        {
                            "execFun": "sendMsg",
                            "data": {
                                "login": "788FEFAD0ED24436AD73D968685110E8",
                                "an": "0AA700EEA5F44D64B9E0243EFB08C4DB",
                                "recipientId": "48A0CA0657DE4FB09CDC86008B2A8EBE",
                                "toGroup": "true",
                                "textMsg": "response for group test message",
                                "guidMsg": "3D45AF8702CA4857A03D7EBD90D95C89"
                            }
                        }
                        */

            OutputSocketMessage output = new OutputSocketMessage("sendMsg", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            InputMsgInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<InputMsgInfo>(val.ToString());
            }
            catch
            {
                output.success = false;
                output.msgError = "Некорректый блок data";
                rez.msg = output;
                return rez;
            }
            
            using (var db = new CloudChatEntities())
            {

                if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                {
                    if (info.toGroup)
                    {
                        if(!db.groups.Any(it => it.group_id == info.recipientId))
                        {
                            output.success = false;
                            output.msgError = "Группа не найдена";
                            rez.msg = output;
                            return rez;
                        }
                    }
                    else if(!db.members.Any(it => it.public_id == info.recipientId))
                    {
                        output.success = false;
                        output.msgError = "Пользователь не найден";
                        rez.msg = output;
                        return rez;
                    }

                    if (output.success)
                    {

                        content_over_8000 msg = new content_over_8000();
                        msg.share_id = info.msgId;
                        msg.file_data = Encoding.Unicode.GetBytes(info.textMsg);
                        db.content_over_8000.Add(msg);

                        shares newShare = new shares();
                        newShare.id = info.msgId;
                        newShare.owner_private = db.usp_membersSelect(info.login).First().private_id;
                        newShare.to_public = info.recipientId;
                        newShare.self_one_or_group = info.toGroup.ToString();
                        newShare.content = msg;

                        newShare.file_extention = "none";

                        db.shares.Add(newShare);
                    
                        db.SaveChanges();


                        if (info.toGroup)
                        {
                            db.group_members.Where(it => it.group_id == info.recipientId).ToList().ForEach(it => rez.usersId.Add(it.member_id));
                        }
                        else
                        {
                            rez.usersId.Add(info.login);
                            rez.usersId.Add(info.recipientId);
                        }

                    }
                }
                else
                {
                    output.success = false;
                    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                    rez.msg = output;
                    return rez;
                }
            }

            rez.msg = output;   
            return rez;
        }

        private OutputSocketMessageWithUsers getmsg(Object val)
        {
            /*
                        {
                            "execFun": "getMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "getAll": "true",     
                                "onGroup": "false",
                                "onlyId": "00000000000000000000000000000000"
                            }
                        }
                         
                        {
                            "execFun": "getMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "getAll": "false",     
                                "onGroup": "true",
                                "onlyId": "48A0CA0657DE4FB09CDC86008B2A8EBE"
                            }
                        }
                        {
                            "execFun": "getMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "getAll": "false",     
                                "onGroup": "false",
                                "onlyId": "788FEFAD0ED24436AD73D968685110E8"
                            }
                        }
                        */

            OutputSocketMessage output = new OutputSocketMessage("getMsg", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            InGetMsgInfo info;
            try
            {
                info = JsonConvert.DeserializeObject<InGetMsgInfo>(val.ToString());
            }
            catch
            {
                output.success = false;
                output.msgError = "Некорректый блок data";
                rez.msg = output;
                return rez;
            }

            using (var db = new CloudChatEntities())
            {

               
                List<OutGetMsgInfo> list = new List<OutGetMsgInfo>();

                if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                {               
                    var owner = db.usp_membersSelect(info.login).Single();

                    var myListGroupId = from m in db.members
                                        join membgr in db.group_members on m.private_id equals membgr.member_id
                                        //join gr in db.groups on membgr.group_id equals gr.group_id
                                        where m.public_id == owner.public_id
                                        select membgr.group_id;

                    if (info.getAll)
                    {

                        var msgInfoList = from s in db.shares
                                        join content in db.content_over_8000 on s.id equals content.share_id
                                        where s.owner_private == owner.private_id ||
                                                (s.to_public == owner.public_id && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase)) ||
                                                (myListGroupId.Contains(s.to_public) && s.self_one_or_group.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                                        select new
                                        {
                                            guidMsg = s.id,
                                            textMsg = content.file_data,
                                            sender = s.owner_private,
                                            gr = s.self_one_or_group
                                        };

                        foreach (var item in msgInfoList)
                        {
                            list.Add(
                                new OutGetMsgInfo(
                                    item.guidMsg,
                                    Encoding.Unicode.GetString(item.textMsg),
                                    db.members.First(it => it.private_id == item.sender).public_id,
                                    item.gr
                                )
                            );
                        }

                    }
                    else
                    {
                        
                        if (info.onGroup)
                        {
                            if (!db.groups.Any(it => it.group_id == info.onlyId) && !myListGroupId.Contains(info.onlyId))
                            {
                                output.success = false;
                                output.msgError = "Группа не найдена";
                                rez.msg = output;
                                return rez;
                            }
                            else {
                                var msgInfoList = from s in db.shares
                                              join content in db.content_over_8000 on s.id equals content.share_id
                                              where (s.to_public == info.onlyId && s.self_one_or_group.Equals("true", StringComparison.InvariantCultureIgnoreCase)) 
                                              select new
                                                      {
                                                          guidMsg = s.id,
                                                          textMsg = content.file_data,
                                                          sender = s.owner_private,
                                                          gr = s.self_one_or_group
                                                      };

                                foreach (var item in msgInfoList)
                                {
                                    list.Add(
                                        new OutGetMsgInfo(
                                            item.guidMsg,
                                            Encoding.Unicode.GetString(item.textMsg),
                                            db.members.First(it => it.private_id == item.sender).public_id,
                                            item.gr
                                        )
                                    );
                                }
                            }
                        }
                        else if(!db.members.Any(it => it.public_id == info.onlyId))
                        {
                            output.success = false;
                            output.msgError = "Пользователь не найден";
                            rez.msg = output;
                            return rez;
                        }
                        else
                        {
                            var msgInfoList = from s in db.shares
                                          join content in db.content_over_8000 on s.id equals content.share_id
                                          where (s.owner_private == owner.private_id && s.to_public == info.onlyId && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase)) ||
                                                (s.owner_private == db.members.FirstOrDefault(it=>it.public_id==info.onlyId).private_id && s.to_public == owner.public_id && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                                          select new
                                          {
                                              guidMsg = s.id,
                                              textMsg = content.file_data,
                                              sender = s.owner_private,
                                              gr = s.self_one_or_group
                                          };

                            foreach (var item in msgInfoList)
                            {
                                list.Add(
                                    new OutGetMsgInfo(
                                        item.guidMsg,
                                        Encoding.Unicode.GetString(item.textMsg),
                                        db.members.First(it => it.private_id == item.sender).public_id,
                                        item.gr
                                    )
                                );
                            }
                        }

                    }

                }
                else
                {
                    output.success = false;
                    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";

                    rez.msg = output;
                    return rez;
                } 
                output.data = list;
            }
            rez.msg = output;
            rez.usersId.Add(info.login);
            return rez;
        }


       
    }



   
}