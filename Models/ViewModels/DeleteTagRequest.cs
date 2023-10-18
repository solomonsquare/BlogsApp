using System;

namespace Bloggee.Models.ViewModels
{
    public class DeleteTagRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
