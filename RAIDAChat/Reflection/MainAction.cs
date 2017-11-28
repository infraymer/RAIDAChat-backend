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

        /// <summary>
        /// Проверка и преобразование входных параметров метода
        /// </summary>
        /// <typeparam name="T">Ожидаемый класс</typeparam>
        /// <param name="data">Объект с проверяемыми данными</param>
        /// <param name="outp">Отображаемое сообщение</param>
        /// <param name="rez">Общий клас объект для возврата данных</param>
        /// <returns>Объект класса Т</returns>
        private T DeserObj<T>(Object data, OutputSocketMessage outp, out OutputSocketMessageWithUsers rez)
        {
            rez = new OutputSocketMessageWithUsers();
            T sucObj = default(T);
            try
            {
                sucObj = JsonConvert.DeserializeObject<T>(data.ToString());
            }
            catch
            {
                outp.success = false;
                outp.msgError = "Некорректый блок data";
                rez.msgForOwner = outp;
            }                       
            return sucObj;
        }


        public AuthInfoWithSocket Auth(object data)
        {
            #region Тестовые данные
            /*
             {
                "execFun": "authorization",
                "data": {
                    "login": "80f7efc032dd4a7c97f69fca51ad3000",
                    "an": "d842703c8acb4bd893876d700f60683e"
                }
             }
             */
            #endregion

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

        private OutputSocketMessageWithUsers registration(Object val, Object nothing )
        {
            #region Тестовые данные
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
            #endregion

            OutputSocketMessage output = new OutputSocketMessage("registration", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            AuthInfo info = DeserObj<AuthInfo>(val, output, out rez);
            if(info == null)
            {
                return rez;
            }
            
            //string vall = val.ToString();
            //AuthInfo info;
            //try
            //{
            //    info = JsonConvert.DeserializeObject<AuthInfo>(vall);
            //}
            //catch 
            //{
            //    output.success = false;
            //    output.msgError = "Некорректый блок data";
            //    rez.msgForOwner = output;
            //    return rez;            
            //}

           
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

            rez.msgForOwner = output;
            return rez;
        }

        private OutputSocketMessageWithUsers creategroup(Object val, Guid myPublicLogin)
        {
            #region Тестовые данные
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
            #endregion

            OutputSocketMessage output = new OutputSocketMessage("createGroup", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            GroupInfo info = DeserObj<GroupInfo>(val, output, out rez);
            if (info == null)
            {
                return rez;
            }

            //GroupInfo info;
            //try
            //{
            //    info = JsonConvert.DeserializeObject<GroupInfo>(val.ToString());
            //}
            //catch
            //{
            //    output.success = false;
            //    output.msgError = "Некорректый блок data";
            //    rez.msgForOwner = output;
            //    return rez;
            //}

            using (var db = new CloudChatEntities())
            {

                //if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                //{
                Guid owner = db.usp_membersSelect(myPublicLogin).First().private_id;
                
                db.usp_groupsInsert(info.public_id, info.name, owner, "", Guid.Empty);
                db.usp_group_membersInsert(info.public_id, owner, "allow_or_deny");
                //}
                //else
                //{
                //    output.success = false;
                //    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                //}
            }

            rez.msgForOwner = output;
            //rez.usersId.Add(info.login);
            return rez;
        }

        private OutputSocketMessageWithUsers addmemberingroup(Object val, Guid myPublicLogin)
        {
            #region Тестовые данные
            /*
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
            #endregion

            OutputSocketMessage output = new OutputSocketMessage("addMemberInGroup", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            AddMemberInGroupInfo info = DeserObj<AddMemberInGroupInfo>(val, output, out rez);
            if (info == null)
            {
                return rez;
            }

            //AddMemberInGroupInfo info;
            //try
            //{
            //    info = JsonConvert.DeserializeObject<AddMemberInGroupInfo>(val.ToString());
            //}
            //catch
            //{
            //    output.success = false;
            //    output.msgError = "Некорректый блок data";
            //    rez.msgForOwner = output;
            //    return rez;
            //}

            using (var db = new CloudChatEntities())
            {

                //if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                //{
                    
                if(db.members.Any(it => it.public_id == info.memberId))
                {
                    Guid owner = db.members.First(it => it.public_id == myPublicLogin).private_id;
                    if (db.group_members.Any(it=>it.member_id==owner && it.group_id==info.groupId))
                    {
                        Guid memberId = db.usp_membersSelect(info.memberId).First().private_id;
                        if (db.group_members.Any(it=> it.group_id==info.groupId && it.member_id == memberId))
                        {
                            output.success = false;
                            output.msgError = "Пользователь уже состоит в данной группе";
                        }
                        else
                        {
                            db.usp_group_membersInsert(info.groupId, memberId, "I don't known");
                            rez.usersId.Add(info.memberId);
                            rez.msgForOther = new { callFunction = "You added in group" };
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
                   
                //}
                //else
                //{
                //    output.success = false;
                //    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                //}
            }

            rez.msgForOwner = output;
            //rez.usersId.Add(info.login);
            
            return rez;
        }

        private OutputSocketMessageWithUsers sendmsg(Object val, Guid myPublicLogin)
        {
            #region Тестовые данные
            /*
                        {
                            "execFun": "sendMsg",
                            "data": {
                                "login": "80f7efc032dd4a7c97f69fca51ad3000",
                                "an": "d842703c8acb4bd893876d700f60683e",
                                "recipientId": "788FEFAD0ED24436AD73D968685110E8",     
                                "toGroup": "false",
                                "textMsg": "test message for one user",
                                "guidMsg": "91D8333FA55B40AFB46CA63E214C93C8",
                                "sendTime": "1511450497620",
                                "curFrg":"1",
                                "totalFrg":"2"
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
                                "guidMsg": "CA5BBD6B488941FEACFA19692D3087E0",
                                "sendTime": "1511450497620",
                                "curFrg":"1",
                                "totalFrg":"2"
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
                                "guidMsg": "19D2B66BB477467ABAE72AC79F5A2C48",
                                "sendTime": "1511450497620",
                                "curFrg":"1",
                                "totalFrg":"2"
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
                                "guidMsg": "3D45AF8702CA4857A03D7EBD90D95C89",
                                "sendTime": "1511450497620",
                                "curFrg":"1",
                                "totalFrg":"2"
                            }
                        }
                        */
            #endregion

            OutputSocketMessage output = new OutputSocketMessage("sendMsg", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            OutGetMsgInfo outputForOther = null;

            InputMsgInfo info = DeserObj<InputMsgInfo>(val, output, out rez);
            if (info == null)
            {
                return rez;
            }

            //InputMsgInfo info;
            //try
            //{
            //    info = JsonConvert.DeserializeObject<InputMsgInfo>(val.ToString());
            //}
            //catch
            //{
            //    output.success = false;
            //    output.msgError = "Некорректый блок data";
            //    rez.msgForOwner = output;
            //    return rez;
            //}

            using (var db = new CloudChatEntities())
            {

                //if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                //{
                if (info.toGroup)
                {
                    if(!db.groups.Any(it => it.group_id == info.recipientId))
                    {
                        output.success = false;
                        output.msgError = "Группа не найдена";
                        rez.msgForOwner = output;
                        return rez;
                    }
                }
                else if(!db.members.Any(it => it.public_id == info.recipientId))
                {
                    output.success = false;
                    output.msgError = "Пользователь не найден";
                    rez.msgForOwner = output;
                    return rez;
                }

                if (output.success)
                {

                    content_over_8000 msg = new content_over_8000();
                    msg.shar_id = info.msgId;
                    msg.file_data = Encoding.Unicode.GetBytes(info.textMsg);
                    db.content_over_8000.Add(msg);

                    shares newShare = new shares();
                    newShare.id = info.msgId;
                    newShare.owner_private = db.usp_membersSelect(myPublicLogin).First().private_id;
                    newShare.to_public = info.recipientId;
                    newShare.self_one_or_group = info.toGroup.ToString();
                    newShare.content = msg;
                    newShare.current_fragment = info.curFrg;
                    newShare.total_fragment = info.totalFrg;
                    newShare.sending_date = info.sendTime;

                    newShare.file_extention = "none";

                    db.shares.Add(newShare);
                    
                    db.SaveChanges();


                    outputForOther = new OutGetMsgInfo(info.msgId, info.textMsg, myPublicLogin, info.toGroup.ToString(), info.recipientId, info.sendTime, info.curFrg, info.totalFrg);

                    if (info.toGroup)
                    {
                        db.group_members.Where(it => it.group_id == info.recipientId && it.member_id != newShare.owner_private)
                                .ToList()
                                .ForEach(it => rez.usersId.Add(
                                                            db.members.First(m=> m.private_id==it.member_id).public_id
                                                            )
                                );
                    }
                    else
                    {
                        //rez.usersId.Add(info.login);
                        rez.usersId.Add(info.recipientId);
                    }

                }
                //}
                //else
                //{
                //    output.success = false;
                //    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";
                //    rez.msgForOwner = output;
                //    return rez;
                //}
            }
            output.data = outputForOther;
            rez.msgForOwner = output;
            rez.msgForOther = output;
           // rez.msgForOther = new { callFunction="newMessage", data = outputForOther } ;
            return rez;
        }

        private OutputSocketMessageWithUsers getmsg(Object val, Guid myPublicLogin)
        {
            #region Тестовые данные
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
            #endregion


            OutputSocketMessage output = new OutputSocketMessage("getMsg", true, "", new { });
            OutputSocketMessageWithUsers rez = new OutputSocketMessageWithUsers();

            InGetMsgInfo info = DeserObj<InGetMsgInfo>(val, output, out rez);
            if (info == null)
            {
                return rez;
            }

            //InGetMsgInfo info;
            //try
            //{
            //    info = JsonConvert.DeserializeObject<InGetMsgInfo>(val.ToString());
            //}
            //catch
            //{
            //    output.success = false;
            //    output.msgError = "Некорректый блок data";
            //    rez.msgForOwner = output;
            //    return rez;
            //}

            using (var db = new CloudChatEntities())
            {

               
                List<OutGetMsgInfo> list = new List<OutGetMsgInfo>();

                //if (db.members.Any(it => it.public_id == info.login && it.an == info.an))
                //{               
                var owner = db.usp_membersSelect(myPublicLogin).Single();

                var myListGroupId = from m in db.members
                                    join membgr in db.group_members on m.private_id equals membgr.member_id
                                    //join gr in db.groups on membgr.group_id equals gr.group_id
                                    where m.public_id == owner.public_id
                                    select membgr.group_id;

                if (info.getAll)
                {

                    var msgInfoList = from s in db.shares
                                    join content in db.content_over_8000 on s.id equals content.shar_id
                                    where s.owner_private == owner.private_id ||
                                            (s.to_public == owner.public_id && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase)) ||
                                            (myListGroupId.Contains(s.to_public) && s.self_one_or_group.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                                    orderby s.sending_date descending
                                    select new
                                    {
                                        guidMsg = s.id,
                                        textMsg = content.file_data,
                                        sender = s.owner_private,
                                        gr = s.self_one_or_group,
                                        recip = s.to_public,
                                        time = s.sending_date,
                                        cur = s.current_fragment,
                                        total = s.total_fragment
                                    };

                    foreach (var item in msgInfoList)
                    {
                        list.Add(
                            new OutGetMsgInfo(
                                item.guidMsg,
                                Encoding.Unicode.GetString(item.textMsg),
                                db.members.First(it => it.private_id == item.sender).public_id,
                                item.gr,
                                item.recip,
                                item.time,
                                item.cur,
                                item.total
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
                            rez.msgForOwner = output;
                            return rez;
                        }
                        else {
                            var msgInfoList = from s in db.shares
                                            join content in db.content_over_8000 on s.id equals content.shar_id
                                            where (s.to_public == info.onlyId && s.self_one_or_group.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                                            orderby s.sending_date descending
                                            select new
                                                    {
                                                        guidMsg = s.id,
                                                        textMsg = content.file_data,
                                                        sender = s.owner_private,
                                                        gr = s.self_one_or_group,
                                                        recip = s.to_public,
                                                        time = s.sending_date,
                                                        cur = s.current_fragment,
                                                        total = s.total_fragment
                                                    };

                            foreach (var item in msgInfoList)
                            {
                                list.Add(
                                    new OutGetMsgInfo(
                                        item.guidMsg,
                                        Encoding.Unicode.GetString(item.textMsg),
                                        db.members.First(it => it.private_id == item.sender).public_id,
                                        item.gr,
                                        item.recip,
                                        item.time,
                                        item.cur,
                                        item.total
                                    )
                                );
                            }
                        }
                    }
                    else if(!db.members.Any(it => it.public_id == info.onlyId))
                    {
                        output.success = false;
                        output.msgError = "Пользователь не найден";
                        rez.msgForOwner = output;
                        return rez;
                    }
                    else
                    {
                        var msgInfoList = from s in db.shares
                                        join content in db.content_over_8000 on s.id equals content.shar_id
                                        where (s.owner_private == owner.private_id && s.to_public == info.onlyId && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase)) ||
                                            (s.owner_private == db.members.FirstOrDefault(it=>it.public_id==info.onlyId).private_id && s.to_public == owner.public_id && s.self_one_or_group.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                                        orderby s.sending_date descending
                                        select new
                                        {
                                            guidMsg = s.id,
                                            textMsg = content.file_data,
                                            sender = s.owner_private,
                                            gr = s.self_one_or_group,
                                            recip = s.to_public,
                                            time = s.sending_date,
                                            cur = s.current_fragment,
                                            total = s.total_fragment
                                        };

                        foreach (var item in msgInfoList)
                        {
                            list.Add(
                                new OutGetMsgInfo(
                                    item.guidMsg,
                                    Encoding.Unicode.GetString(item.textMsg),
                                    db.members.First(it => it.private_id == item.sender).public_id,
                                    item.gr,
                                    item.recip,
                                    item.time,
                                    item.cur,
                                    item.total
                                )
                            );
                        }
                    }

                }

                //}
                //else
                //{
                //    output.success = false;
                //    output.msgError = "Пользователь не авторизирован. Неправильный логин или пароль";

                //    rez.msgForOwner = output;
                //    return rez;
                //} 
                output.data = list;
            }
            rez.msgForOwner = output;
            //rez.usersId.Add(info.login);
            return rez;
        }


       
    }



   
}