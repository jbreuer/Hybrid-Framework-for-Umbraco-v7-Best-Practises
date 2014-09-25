using Archetype.Models;
using nuPickers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Zbu.ModelsBuilder;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Contact
    {
        [ImplementPropertyType("email")]
        public IEnumerable<EmailFields> Email
        {
            get
            {
                var archetypeModel = this.GetPropertyValue<ArchetypeModel>("email");
                return archetypeModel.Select(x =>
                    {
                        return new EmailFields()
                        {
                            Send = x.GetValue<bool>("send"),
                            SenderName = x.GetValue<string>("senderName"),
                            SenderEmail = x.GetValue<string>("senderEmail"),
                            Subject = x.GetValue<string>("subject"),
                            ReceiverEmail = x.GetValue<string>("receiverEmail"),
                            CcEmail = x.GetValue<string>("ccEmail"),
                            BccEmail = x.GetValue<string>("bccEmail"),
                            Body = x.GetValue<string>("body")
                        };
                    }
                ).ToList();
            }
        }
    }
}