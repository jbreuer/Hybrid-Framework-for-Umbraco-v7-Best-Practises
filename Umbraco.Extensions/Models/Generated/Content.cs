using Archetype.Models;
using nuPickers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Zbu.ModelsBuilder;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Content
    {
        [ImplementPropertyType("file1")]
        public File File1
        {
            get
            {
                var file = this.GetPropertyValue<IPublishedContent>("file1") as File;
                return file;
            }
        }

        [ImplementPropertyType("files1")]
        public IEnumerable<File> Files1
        {
            get
            {
                var files = this.GetPropertyValue<IEnumerable<IPublishedContent>>("files1");
                if(files != null)
                {
                    return files.Select(x => x as File).ToList();
                }
                return null;
            }
        }

        [ImplementPropertyType("mntpNews")]
        public IEnumerable<Newsitem> MntpNews
        {
            get
            {
                var newsItems = this.GetPropertyValue<IEnumerable<IPublishedContent>>("mntpNews");
                if (newsItems != null)
                {
                    return newsItems.Select(x => x as Newsitem).ToList();
                }
                return null;
            }
        }

        [ImplementPropertyType("enumCheckboxList")]
        public IEnumerable<Packages> EnumCheckboxList
        {
            get
            {
                var picker = this.GetPropertyValue<Picker>("enumCheckboxList");
                return picker.AsEnums<Packages>();
            }
        }

        [ImplementPropertyType("memberPicker")]
        public IEnumerable<Member> MemberPicker
        {
            get
            {
                var picker = this.GetPropertyValue<Picker>("memberPicker");
                return picker.AsPublishedContent().Select(x => x as Member).ToList();
            }
        }

        [ImplementPropertyType("multiUrlPicker")]
        public IEnumerable<UrlPicker.Umbraco.Models.UrlPicker> MultiUrlPicker
        {
            get
            {
                var archetypeModel = this.GetPropertyValue<ArchetypeModel>("multiUrlPicker");
                return archetypeModel.Select(x => x.GetValue<UrlPicker.Umbraco.Models.UrlPicker>("urlPicker")).ToList();
            }
        }

        [ImplementPropertyType("multipleTextbox")]
        public IEnumerable<string> MultipleTextbox
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>("multipleTextbox").Select(x => x.GetValue<string>("name"));
            }
        }
    }
}