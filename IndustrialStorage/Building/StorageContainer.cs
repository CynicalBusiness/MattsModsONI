using System;
using KSerialization;

namespace MattsMods.IndustrialStorage.Building
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class StorageContainer : KMonoBehaviour
    {

        private int totalColors = 5;

        [Serialize]
        private int currentColor = -1;

        private VariantController colorVariants;

        public int TotalColors
        {
            get
            {
                return totalColors;
            }
            set
            {
                totalColors = value;
                UpdateColorController();
            }
        }

        public int CurrentColor
        {
            get {
                return currentColor;
            }
            set {
                currentColor = value;
                UpdateColor();
            }
        }

        /// <summary>
        /// Sets a random color variant for this container.
        /// </summary>
        public void SetRandomVariant ()
        {
            SetRandomVariant(null);
        }

        /// <summary>
        /// Sets a random color variant for this container using the specified random.
        /// </summary>
        /// <param name="random">The random to use</param>
        public void SetRandomVariant (Random random)
        {
            if (random == null) random = new Random();
            CurrentColor = random.Next(0, totalColors);
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            SetLadders(true);
            SetFloor(true);

            UpdateColorController();
            UpdateColor();
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
                Grid.HasLadder[cell] = toggle;
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

        private void UpdateColorController ()
        {
            colorVariants = new VariantController(this, "color_target", "color", totalColors, Grid.SceneLayer.BuildingBack);
        }

        private void UpdateColor ()
        {
            if (currentColor < 0)
            {
                SetRandomVariant();
            }
            else
            {
                colorVariants.SetVariant(CurrentColor);
            }
        }
    }
}
