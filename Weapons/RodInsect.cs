﻿using MHW_Editor.Assets;
using MHW_Editor.Models;
using MHW_Template;

namespace MHW_Editor.Weapons {
    public partial class RodInsect : MhwItem {
        public RodInsect(byte[] bytes, ulong offset) : base(bytes, offset) {
        }

        public override string Name => DataHelper.kinsectNames[MainWindow.locale].TryGet((byte) Index);
    }
}