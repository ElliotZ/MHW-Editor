using System;
using System.ComponentModel;
using MHW_Editor.Assets;
using MHW_Editor.Models;
using MHW_Template;
using MHW_Template.Models;

namespace MHW_Editor.Weapons {
    public partial class WeaponWhistle {
        public const uint StructSize = 7;
        public const ulong InitialOffset = 10;
        public const long EntryCountOffset = 6;
        public const string EncryptionKey = null;
        public override string UniqueId => $"{Id}";

        public const string Id_displayName = "Id";
        public const int Id_sortIndex = 50;
        [SortOrder(Id_sortIndex)]
        [DisplayName(Id_displayName)]
        [IsReadOnly]
        public virtual uint Id {
            get => GetData<uint>(0);
            set {
                if (GetData<uint>(0) == value) return;
                SetData(0, value, nameof(Id));
                OnPropertyChanged(nameof(Raw_Data));
                OnPropertyChanged(nameof(Id));
            }
        }

        public const string Note_1_displayName = "Note 1";
        public const int Note_1_sortIndex = 100;
        [SortOrder(Note_1_sortIndex)]
        [DisplayName(Note_1_displayName)]
        public virtual MHW_Template.Weapons.NoteColor Note_1 {
            get => (MHW_Template.Weapons.NoteColor) GetData<byte>(4);
            set {
                if ((MHW_Template.Weapons.NoteColor) GetData<byte>(4) == value) return;
                SetData(4, (byte) value, nameof(Note_1));
                OnPropertyChanged(nameof(Raw_Data));
                OnPropertyChanged(nameof(Note_1));
            }
        }

        public const string Note_2_displayName = "Note 2";
        public const int Note_2_sortIndex = 150;
        [SortOrder(Note_2_sortIndex)]
        [DisplayName(Note_2_displayName)]
        public virtual MHW_Template.Weapons.NoteColor Note_2 {
            get => (MHW_Template.Weapons.NoteColor) GetData<byte>(5);
            set {
                if ((MHW_Template.Weapons.NoteColor) GetData<byte>(5) == value) return;
                SetData(5, (byte) value, nameof(Note_2));
                OnPropertyChanged(nameof(Raw_Data));
                OnPropertyChanged(nameof(Note_2));
            }
        }

        public const string Note_3_displayName = "Note 3";
        public const int Note_3_sortIndex = 200;
        [SortOrder(Note_3_sortIndex)]
        [DisplayName(Note_3_displayName)]
        public virtual MHW_Template.Weapons.NoteColor Note_3 {
            get => (MHW_Template.Weapons.NoteColor) GetData<byte>(6);
            set {
                if ((MHW_Template.Weapons.NoteColor) GetData<byte>(6) == value) return;
                SetData(6, (byte) value, nameof(Note_3));
                OnPropertyChanged(nameof(Raw_Data));
                OnPropertyChanged(nameof(Note_3));
            }
        }

        public const int lastSortIndex = 250;
    }
}