using MagicVilla.MagicApi.Model.DTO;

namespace MagicVilla.MagicApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO> {
                new VillaDTO { Id = 1, Name = "Pool View" },
                new VillaDTO{Id=2, Name="Beach View"}
            };
    }
}
