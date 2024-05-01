using Microsoft.AspNetCore.Components;

namespace C3P1.Client.Components.Apps.Cat
{
    public abstract class BaseCat : ComponentBase
    {
        protected List<Cat>? cats;
        protected IEnumerable<Cat>? Cats
        {
            get
            {
                if (cats != null)
                {
                    var query = from c in cats select c;

                    return query;
                }
                else
                {
                    return null;
                }
            }
        }
        protected Cat? activeCat;
    }
}
