using System;
using System.Text;
using HackAndSlash.Entity;
using HackAndSlash.Space;
using HackAndSlash.Session;
using HackAndSlash;
using libtcod;

namespace HackAndSlash.Actor
{
    [Serializable]
    public class Creature
    {
        public enum CreatureType { None = -1, Humanoid = 0, Undead = 1, Draconic = 2, Animal = 3 };
        public enum CreatureFaction { None = -1, PC = 0, Friendly = 2, Enemy = 3 }
        public enum CreatureRace
        {
            None = -1, Human = 0, Kobold = 1, Goblin = 2, Ogre = 3,
            Troll = 4, Golem = 5, Giant = 6, Dragon = 7, Wolf = 8, Bear = 9
        };
        public enum CreatureStatus { None = -1, Skip = 0, Alive = 1, Sleeping = 2, Dead = 3 }
        public enum HeroClass { None = -1, Rogue = 0, Fighter = 1, Wizard = 2 };

        // Default Constructor
        public Creature()
        {
            // Set the internal values
            name = String.Empty;
            strength = 0;
            wisdom = 0;
            dexterity = 0;
            faction = CreatureFaction.None;
            yourClass = HeroClass.None;
            type = CreatureType.None;
            Inventory = new int[27];
            for (int i = 1; i <= 26; i++)
            {
                Inventory[i] = -1;
            }
            graphic = ' ';
            color = new TCODColor(0, 0, 0);
            status = CreatureStatus.None;
            attackDescription = string.Empty;
            speech = string.Empty;
            action = string.Empty;
            currenthp = 0;
            location = new Point(0, 0);
        }

        public Creature(HeroClass hero)
        {
            // Set the internal values
            Inventory = new int[27];
            for (int i = 0; i <= 26; i++)
            {
                Inventory[i] = -1;
            }

            if (hero == HeroClass.Rogue)
            {
                name = "Rogue";
                strength = 5;
                wisdom = 5;
                dexterity = 8;
                hp = dexterity * 2;
                currenthp = hp;
                mp = 0;
                faction = CreatureFaction.PC;
                yourClass = HeroClass.Rogue;
                type = CreatureType.Humanoid;
                graphic = '@';
                color = new TCODColor(0, 0, 0);
                status = CreatureStatus.Alive;
                attackDescription = string.Empty;
                speech = string.Empty;
                action = string.Empty;
                location = new Point(0, 0);
            }
            else if (hero == HeroClass.Fighter)
            {
                name = "Fighter";
                strength = 10;
                wisdom = 5;
                dexterity = 5;
                hp = dexterity * 2;
                currenthp = hp;
                mp = 0;
                faction = CreatureFaction.PC;
                yourClass = HeroClass.Fighter;
                type = CreatureType.Humanoid;
                graphic = '@';
                color = new TCODColor(0, 0, 0);
                status = CreatureStatus.Alive;
                attackDescription = string.Empty;
                speech = string.Empty;
                action = string.Empty;
                location = new Point(0, 0);
            }
            else if (hero == HeroClass.Wizard)
            {
                name = "Wizard";
                strength = 5;
                wisdom = 10;
                dexterity = 5;
                hp = dexterity * 2;
                currenthp = hp;
                mp = 0;
                faction = CreatureFaction.PC;
                yourClass = HeroClass.Wizard;
                type = CreatureType.Humanoid;
                graphic = '@';
                color = new TCODColor(0, 0, 0);
                status = CreatureStatus.Alive;
                attackDescription = string.Empty;
                speech = string.Empty;
                action = string.Empty;
                location = new Point(0, 0);
            }
            else
            {
                name = String.Empty;
                strength = 0;
                wisdom = 0;
                dexterity = 0;
                faction = CreatureFaction.None;
                type = CreatureType.None;
                graphic = ' ';
                color = new TCODColor(0, 0, 0);
                status = CreatureStatus.None;
                attackDescription = string.Empty;
                speech = string.Empty;
                action = string.Empty;
                location = new Point(0, 0);
            }
        }

        public Creature(int Level)
        {
            CreatureRace race = (CreatureRace)Level;

            // Set the internal values
            Inventory = new int[27];
            for (int i = 0; i <= 26; i++)
            {
                Inventory[i] = -1;
            }
            location = new Point(0, 0);

            strength = Convert.ToInt32(race) * 2;
            wisdom = Convert.ToInt32(race) * 2;
            dexterity = Convert.ToInt32(race) * 2;
            status = CreatureStatus.Sleeping;
            color = new TCODColor(0, 0, 0);

            hp = strength * 2;
            currenthp = hp;

            faction = CreatureFaction.Enemy;
            yourClass = HeroClass.None;

            switch (race)
            {
                case CreatureRace.Kobold:
                    type = CreatureType.Humanoid;
                    color = TCODColor.brass;
                    graphic = 'k';
                    attackDescription = "swings at";
                    speech = "I'm gonna kill you!  Yip yip!";
                    action = "picks a chunk of rotten meat from its teeth";
                    break;
                case CreatureRace.Goblin:
                    type = CreatureType.Humanoid;
                    color = TCODColor.grey;
                    graphic = 'g';
                    attackDescription = "stabs at";
                    speech = "This is mine.  All mine!";
                    action = "scratches its bottom";
                    break;
                case CreatureRace.Ogre:
                    type = CreatureType.Humanoid;
                    color = TCODColor.yellow;
                    graphic = 'O';
                    attackDescription = "swings at";
                    speech = "Ugh";
                    action = "scratches its head";
                    break;
                case CreatureRace.Troll:
                    type = CreatureType.Humanoid;
                    color = TCODColor.grey;
                    graphic = 'T';
                    attackDescription = "claws at";
                    speech = "Muuugh";
                    action = "roars";
                    break;
                case CreatureRace.Golem:
                    type = CreatureType.Humanoid;
                    color = TCODColor.blue;
                    graphic = 'G';
                    attackDescription = "mauls";
                    speech = "I'll roll over you!";
                    action = "makes a creaking sound";
                    break;
                case CreatureRace.Giant:
                    type = CreatureType.Humanoid;
                    color = TCODColor.grey;
                    graphic = 'G';
                    attackDescription = "punches";
                    speech = "Gonna squish you!";
                    action = "stomps around, causing the floor to shake";
                    break;
                case CreatureRace.Dragon:
                    type = CreatureType.Draconic;
                    color = TCODColor.red;
                    graphic = 'D';
                    attackDescription = "bites";
                    speech = "We're the only ones that are going to survive!";
                    action = "arches its back and spreads its wings";
                    break;
                case CreatureRace.Wolf:
                    type = CreatureType.Animal;
                    color = TCODColor.silver;
                    graphic = 'w';
                    attackDescription = "bites";
                    speech = "Howls!";
                    action = "bristles and raises it's haunches";
                    break;
                case CreatureRace.Bear:
                    type = CreatureType.Animal;
                    color = TCODColor.orange;
                    graphic = 'B';
                    attackDescription = "mauls";
                    speech = "Roar";
                    action = "rises on it's hind legs";
                    break;
                default:
                    break;
            }

            name = Convert.ToString(race);

        }

        public string Value
        {
            get
            {
                StringBuilder buffer = new StringBuilder(name);
                buffer.Append(":");
                buffer.Append(strength.ToString());
                buffer.Append(":");
                buffer.Append(wisdom.ToString());
                buffer.Append(":");
                buffer.Append(dexterity.ToString());
                buffer.Append(":");
                buffer.Append(hp.ToString());
                buffer.Append(":");
                buffer.Append(currenthp.ToString());
                buffer.Append(":");
                buffer.Append(mp.ToString());
                buffer.Append(":");
                buffer.Append(graphic.ToString());
                buffer.Append(":");
                buffer.Append(color.Red.ToString());
                buffer.Append(":");
                buffer.Append(color.Green.ToString());
                buffer.Append(":");
                buffer.Append(color.Blue.ToString());
                buffer.Append(":");
                for (int i = 1; i <= 26; i++)
                {
                    buffer.Append(Inventory[i].ToString());
                    buffer.Append(":");
                }
                buffer.Append(faction.ToString());
                buffer.Append(":");
                buffer.Append(type.ToString());
                buffer.Append(":");
                buffer.Append(status.ToString());
                buffer.Append(":");
                buffer.Append(attackDescription.ToString());
                buffer.Append(":");
                buffer.Append(speech.ToString());
                buffer.Append(":");
                buffer.Append(action.ToString());
                buffer.Append(":");
                buffer.Append(location.Value.ToString());

                return buffer.ToString();
            }
            set
            {
                int index = 0;
                string[] words = value.Split(':');
                name = words[index++];
                strength = Convert.ToInt32(words[index++]);
                wisdom = Convert.ToInt32(words[index++]);
                dexterity = Convert.ToInt32(words[index++]);
                hp = Convert.ToInt32(words[index++]);
                currenthp = Convert.ToInt32(words[index++]);
                mp = Convert.ToInt32(words[index++]);
                graphic = Convert.ToChar(words[index++]);
                color.Red = Convert.ToByte(words[index++]);
                color.Green = Convert.ToByte(words[index++]);
                color.Blue = Convert.ToByte(words[index++]);
                for (int i = 1; i <= 26; i++)
                {
                    Inventory[i] = Convert.ToInt32(words[index++]);
                }
                faction = (CreatureFaction)Enum.Parse(typeof(CreatureFaction), words[index++]);
                type = (CreatureType)Enum.Parse(typeof(CreatureType), words[index++].Trim('\0'));
                status = (CreatureStatus)Enum.Parse(typeof(CreatureStatus), words[index++].Trim('\0'));
                attackDescription = words[index++];
                speech = words[index++];
                action = words[index++];
                location.Value = words[index++];
            }
        }

        // Private data members
        private string name;
        private int strength;
        private int wisdom;
        private int dexterity;
        private int hp;
        private int currenthp;
        private int mp;
        private CreatureFaction faction;
        private HeroClass yourClass;
        private CreatureType type;
        private char graphic;
        private TCODColor color;
        private CreatureStatus status;
        private string attackDescription;
        private string speech;
        private string action;
        private Point location;
        private Window heroClass;

        public Window ClassHero
        {
            get { return heroClass; }
            set { heroClass = value; }
        }

        internal Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Action
        {
            get { return "The " + name + " " + action; }
            set { action = value; }
        }

        public string AttackDescription
        {
            get { return attackDescription; }
            set { attackDescription = value; }
        }
        public string Speech
        {
            get { return "The " + name + " says '" + speech + "'"; }
            set { speech = value; }
        }

        public int Currenthp
        {
            get { return currenthp; }
            set { currenthp = value; }
        }

        // Inventory
        public int[] Inventory;

        // Get first empty slot
        public int FirstEmptySlot()
        {
            int FirstSlot = -1;
            for (int i = 1; i <= 26; i++)
            {
                if (Inventory[i] == -1)
                {
                    FirstSlot = i;
                    break;
                }
            }
            return FirstSlot;
        }

        public int AddItem(int ItemID)
        {
            int SlotToUse = FirstEmptySlot();
            if (SlotToUse != -1)
            {
                Inventory[SlotToUse] = ItemID;
                return SlotToUse;
            }
            else
            {
                return -1;
            }
        }

        public bool TakeDamage(int Damage)
        {
            // handle regen
            currenthp -= Damage;
            if (currenthp > HP)
            {
                currenthp = HP;
            }

            return (currenthp > 0);
        }

        public bool DropItem(int SlotID)
        {
            if (SlotID < 1 && SlotID > 26)
            {
                return false;
            }
            else
            {
                Inventory[SlotID] = -1;
                return true;
            }
        }

        // Publically accessible properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int AttackBonus
        {
            get
            {
                if (faction == CreatureFaction.PC)
                {
                    return strength;
                }
                else
                {
                    return strength;
                }
            }
        }

        public int DefenseBonus
        {
            get
            {
                if (faction == CreatureFaction.PC)
                {
                    return dexterity;
                }
                else
                {
                    return dexterity;
                }
            }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public int Wisdom
        {
            get { return wisdom; }
            set { wisdom = value; }
        }

        public int Dexterity
        {
            get { return dexterity; }
            set { dexterity = value; }
        }

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public int MP
        {
            get { return mp; }
            set { hp = value; }
        }

        public CreatureFaction Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public HeroClass YourClass
        {
            get { return yourClass; }
            set { yourClass = value; }
        }

        public CreatureType Type
        {
            get { return type; }
            set { type = value; }
        }

        public char Graphic
        {
            get { return graphic; }
            set { graphic = value; }
        }

        public TCODColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public CreatureStatus Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
