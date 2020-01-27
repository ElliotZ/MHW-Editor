﻿using System;
using MHW_Editor.Assets;
using MHW_Editor.Models;
using MHW_Template;

namespace MHW_Editor.Weapons {
    public partial class Melee : MhwItem, IWeapon, ISlots {
        private readonly string weaponFilename;

        public Melee(byte[] bytes, ulong offset, string weaponFilename) : base(bytes, offset) {
            this.weaponFilename = weaponFilename;
        }

        public override string Name => DataHelper.weaponData.TryGet(weaponFilename, DataHelper.dummyDict).TryGet(GMD_Name_Index, "Unknown");

        public bool Is_Fixed_Upgrade {
            get => Convert.ToBoolean(Is_Fixed_Upgrade_Raw);
            set => Is_Fixed_Upgrade_Raw = Convert.ToByte(value);
        }
    }
}