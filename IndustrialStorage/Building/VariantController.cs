using System;
using UnityEngine;

namespace MattsMods.IndustrialStorage.Building
{

    public class VariantController
    {

        public readonly GameObject gameObject;
        public readonly KBatchedAnimController variantController;
        public readonly KBatchedAnimTracker variantTracker;
        public readonly KAnimLink variantLink;
        public readonly int totalVariants;

        // TODO allow Meter.Offset

        public VariantController(
            KMonoBehaviour target,
            string variantTarget,
            string variantAnimation,
            int totalVariants,
            Grid.SceneLayer renderLayer
        ) : this(
            target.GetComponent<KBatchedAnimController>(),
            variantTarget,
            variantAnimation,
            totalVariants,
            renderLayer
        ) {}

        public VariantController(
            KAnimControllerBase controllerBase,
            string variantTarget,
            string variantAnimation,
            int totalVariants,
            Grid.SceneLayer renderLayer
        )
        {
            this.totalVariants = totalVariants;

            string name = $"{controllerBase.name}.{variantAnimation}";
            gameObject = UnityEngine.Object.Instantiate<GameObject>(Assets.GetPrefab(MeterConfig.ID));
            gameObject.name = name;
            gameObject.SetActive(false);
            gameObject.transform.parent = controllerBase.transform;
            gameObject.GetComponent<KPrefabID>().PrefabTag = new Tag(name);

            var position = controllerBase.transform.GetPosition();
            position.z = Grid.GetLayerZ(renderLayer);
            gameObject.transform.SetPosition(position);

            variantController = gameObject.GetComponent<KBatchedAnimController>();
            variantController.AnimFiles = new KAnimFile[]{
                controllerBase.AnimFiles[0]
            };
            variantController.initialAnim = variantAnimation;
            variantController.fgLayer = Grid.SceneLayer.NoLayer;
            variantController.initialMode = KAnim.PlayMode.Paused;
            variantController.isMovable = true;
            variantController.FlipX = controllerBase.FlipX;
            variantController.FlipY = controllerBase.FlipY;
            variantController.sceneLayer = renderLayer;

            variantTracker = gameObject.GetComponent<KBatchedAnimTracker>();
            // variantTracker.offset = offset;
            variantTracker.symbol = variantTarget;

            gameObject.SetActive(true);

            controllerBase.SetSymbolVisiblity(variantTarget, false);

            variantLink = new KAnimLink(controllerBase, variantController);
        }

        /// <summary>
        /// Sets the current variant index for this controller
        /// </summary>
        /// <param name="variantIdx">The index to set to</param>
        public void SetVariant (int variantIdx)
        {
            if (variantController == null) return;
            variantController.SetPositionPercent((float)Math.Max(0, Math.Min(totalVariants, variantIdx) / (float)totalVariants));
        }

        public void SetSymbolTint (KAnimHashedString symbol, Color32 color)
        {
            if (variantController == null) return;
            variantController.SetSymbolTint(symbol, color);
        }

        public void SetRotation (float rot)
        {
            if (variantController == null) return;
            variantController.Rotation = rot;
        }
    }
}
