using System.Collections.Generic;
using Pearl.Multitags;

namespace Pearl.Trigger
{
    public interface ITrigger
    {
        void TriggerEnter(Container informations, List<Tags> tags);

        void TriggerExit(List<Tags> tags);
    }
}
