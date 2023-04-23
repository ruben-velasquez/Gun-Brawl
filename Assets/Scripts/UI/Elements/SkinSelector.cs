using UnityEngine;

namespace UI
{
    public class SkinSelector : Selector
    {
        public SkinList skinList;

        public override void Start()
        {
            base.Start();

            maxValue = skinList.skins.Capacity - 1;

            onChange += UpdateSkin;
        }

        public void UpdateSkin()
        {
            textContent.text = skinList.skins[value].name;

            imageContent.sprite = skinList.skins[value].image;

            GameManager.Instance.SetPlayerSkin(id, skinList.skins[value]);
        }
    }
}