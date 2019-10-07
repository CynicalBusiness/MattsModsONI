using System;
using System.Collections.Generic;
using UnityEngine;
using static MattsMods.IndustrializationFundementals.IndustrializationFundementalsMod;

namespace MattsMods.IndustrializationFundementals.Building
{
    public class Scaffolding : Ladder
    {
        private ObjectLayer GetLayer ()
        {
            return GetComponent<global::Building>().Def.ObjectLayer;
        }

        private GameObject GetBuildingAtOffset (int cell, int x, int y)
        {
            return Grid.Objects[Grid.OffsetCell(cell, new CellOffset(x, y)), (int)GetLayer()];
        }

        private bool GetNeighborAt (int x, int y, Func<GameObject, bool> predicate, out GameObject above)
        {
            var cell = Grid.PosToCell(this);
            above = GetBuildingAtOffset(cell, x, y);
            return above != null && above.GetComponent<Scaffolding>() != null && (predicate == null || predicate.Invoke(above));
        }

        private IEnumerable<GameObject> GetNearestNeighbors (List<GameObject> alreadyChecked, Func<GameObject, bool> predicate = null)
        {
            if (GetNeighborAt(0, -1, predicate, out var below) && !alreadyChecked.Contains(below))
            {
                yield return below;
            }
            if (GetNeighborAt(1, 0, predicate, out var right) && !alreadyChecked.Contains(right))
            {
                yield return right;
            }
            if (GetNeighborAt(-1, 0, predicate, out var left) && !alreadyChecked.Contains(left))
            {
                yield return left;
            }
            if (GetNeighborAt(0, 1, predicate, out var above) && !alreadyChecked.Contains(above))
            {
                yield return above;
            }
        }

        public IEnumerable<GameObject> GetNeighbors (List<GameObject> alreadyChecked, Func<GameObject, bool> predicate = null)
        {
            foreach (var n in GetNearestNeighbors(alreadyChecked, predicate))
            {
                ModLogger.Debug("found neighbor at: {0}", Grid.CellToPos(Grid.PosToCell(n)).ToString());
                alreadyChecked.Add(n);
                yield return n;
            }
        }

        public bool IsMarkedForDeconstruction ()
        {
            var d = GetComponent<Deconstructable>();
            return d != null && d.IsMarkedForDeconstruction();
        }

        public GameObject GetFurthestMarkedForDeconstruct (List<GameObject> alreadyChecked = null)
        {
            if (alreadyChecked == null)
            {
                alreadyChecked = new List<GameObject>();
                alreadyChecked.Add(this.gameObject);
            }

            ModLogger.Debug("looking for neighbors to: {0}", Grid.CellToPos(Grid.PosToCell(this)).ToString());
            foreach (var n in GetNeighbors(alreadyChecked, go =>
            {
                var d = go.GetComponent<Deconstructable>();
                return go.GetComponent<Scaffolding>() != null && d != null && d.IsMarkedForDeconstruction();
            }))
            {
                n.GetComponent<Scaffolding>().GetFurthestMarkedForDeconstruct(alreadyChecked);
            }

            ModLogger.Debug("found total neighbors: {0}", alreadyChecked.Count);
            return alreadyChecked[alreadyChecked.Count - 1];
        }
    }
}
