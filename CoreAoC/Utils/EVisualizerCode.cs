using System.ComponentModel.DataAnnotations;

namespace CoreAoC.Utils
{
    internal enum EVisualizerCode
    {
        [Display(Name = "Resolver todo")]
        EV_1,
        [Display(Name = "Resolver un calendario completo")]
        EV_2,
        [Display(Name = "Resolver un problema concreto")]
        EV_3,
        [Display(Name = "Salir")]
        EV_EXIT,
    }
}
