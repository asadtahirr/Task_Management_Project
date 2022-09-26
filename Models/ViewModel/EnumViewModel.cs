using Microsoft.AspNetCore.Mvc.Rendering;

namespace project_management_system.Models.ViewModel
{
    public class EnumViewModel
    {
        public Priority SelectedPriority { get; set; }
        public List<SelectListItem> PrioritySelectedItems { get; set; }
        public EnumViewModel(ICollection<Priority> priority)
        {
            PrioritySelectedItems = new List<SelectListItem>();
            foreach(Priority p in priority)
            {
                PrioritySelectedItems.Add(new SelectListItem(p.ToString(), p.ToString()));
            }
        }
    }
}
