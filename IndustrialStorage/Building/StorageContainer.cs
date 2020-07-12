
namespace MattsMods.IndustrialStorage.Building
{
    public class StorageContainer : KMonoBehaviour
    {



        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            SetLadders(true);
            SetFloor(true);
        }

        protected override void OnCleanUp()
        {
            base.OnCleanUp();
            SetLadders(false);
            SetFloor(false);
        }

        protected void SetLadders (bool toggle)
        {
            foreach (var cell in GetLeftCells())
            {
                Grid.HasLadder[Grid.OffsetCell(cell, new CellOffset(1, 0))] = toggle;
            }
        }

        protected void SetFloor (bool toggle)
        {
            foreach (var cell in GetTopCells())
            {
                Grid.FakeFloor[cell] = toggle;
            }
        }

        private BuildingDef GetBuildingDef()
        {
            return GetComponent<global::Building>().Def;
        }

        private int GetLeftOffset ()
        {
            var buildingDef = GetBuildingDef();
            var isEven = buildingDef.WidthInCells % 2 == 0;
            var half = GetBuildingDef().WidthInCells / 2;
            return -(isEven ? half - 1 : half);
        }

        private int GetRightOffset ()
        {
            return GetBuildingDef().WidthInCells / 2;
        }

        private int GetBottomLeftCell ()
        {
            return Grid.OffsetCell(Grid.PosToCell(this), new CellOffset(GetLeftOffset(), 0));
        }

        private int[] GetLeftCells ()
        {
            var buildingDef = GetBuildingDef();
            var left = GetLeftOffset();
            var cells = new int[buildingDef.HeightInCells];
            for (var i = 0; i < cells.Length; i++)
            {
                cells[i] = Grid.OffsetCell(Grid.PosToCell(this), new CellOffset(GetLeftOffset(), i));
            }
            return cells;
        }

        private int[] GetTopCells ()
        {
            var buildingDef = GetBuildingDef();
            var left = GetLeftOffset();
            var cells = new int[buildingDef.WidthInCells];
            for (var i = 0; i < cells.Length; i++)
            {
                cells[i] = Grid.OffsetCell(Grid.PosToCell(this), new CellOffset(left + i, buildingDef.HeightInCells - 1));
            }
            return cells;
        }
    }
}
