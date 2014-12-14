﻿using Alienseed.BaseNetworkServer.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeenPhone.Client
{
    class ClientPacketHandler : BasePacketHandler
    {
        protected override void UserLogin(IUser user)
        {
            EventSource.InvokeOnUserConnected(this, user);
        }

        protected override void UserLogout(IUser user)
        {
            EventSource.InvokeOnUserDisconnected(this, user);
        }
        
        protected override void OnChat(IUser user, string text)
        {
            EventSource.InvokeOnChat(this, user, text);
        }

        protected override void OnLoginStatus(bool isLoggedIn)
        {
            EventSource.InvokeOnLoginStatus(this, isLoggedIn);
        }

        protected override void LoginInfo(string username, string password)
        {
            Console.WriteLine("Invalid client packet LoginInfo received.");
        }

        protected override void UserList(IEnumerable<IUser> users)
        {
            EventSource.InvokeOnUserList(this, users);
        }

        protected override IUser GetUserObject(Guid id, bool isadmin, string username, string nickname)
        {
            return UserRepo.CreateOrUpdateUser(id, isadmin, username, nickname);
        }
    }
}
