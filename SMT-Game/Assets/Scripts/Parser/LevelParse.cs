using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Parser
{
    public class ObjectType
    {
        static ObjectType[] types = new ObjectType[]
        {
            new ObjectType(0, false, ObjectType_enum.cloud),
            new ObjectType(1, false, ObjectType_enum.bomb),
            new ObjectType(2, false, ObjectType_enum.star),
            new ObjectType(3, false, ObjectType_enum.Float),
            new ObjectType(4, false, ObjectType_enum.Integer),
            new ObjectType(5, false, ObjectType_enum.String),
             new ObjectType(6, true, ObjectType_enum.raft)
        };
        static ObjectType[] arrayTypes = new ObjectType[]
        {
            new ObjectType(0, true, ObjectType_enum.cloud),
            new ObjectType(1, true, ObjectType_enum.bomb),
            new ObjectType(2, true, ObjectType_enum.star),
            new ObjectType(3, true, ObjectType_enum.Float),
            new ObjectType(4, true, ObjectType_enum.Integer),
            new ObjectType(5, true, ObjectType_enum.String),
             new ObjectType(6, true, ObjectType_enum.raft)
        };
        string stringValue;
        int idx;
        bool isArray;

        public override string ToString()
        {
            return stringValue;
        }
        
        public bool MatchBaseType(ObjectType other)
        {
            if (other == null)
                return false;
            return idx == other.idx;
        }

        public static ObjectType Array(ObjectType value)
        {
            if (value == null)
                return null;
            if (value.isArray)
                return null;
            else
                return arrayTypes[value.idx];
        }

        public static ObjectType UnArray(ObjectType value)
        {
            if (value == null)
                return null;
            if (value.isArray)
                return types[value.idx];
            else
                return null;
        }

        public static ObjectType Parse(string str)
        {
            bool isArray = false;
            if(str.EndsWith("[]"))
            {
                str = str.Replace("[]", "");
                isArray = true;
            }
            try
            {
                int idx = (int)Enum.Parse(typeof(ObjectType_enum), str) - 1;
                if (isArray)
                    return arrayTypes[idx];
                else
                    return types[idx];
            }
            catch
            {
                return null;
            }

        }

        public static ObjectType cloud { get { return types[0]; } }
        public static ObjectType bomb { get { return types[1]; } }
       
        public static ObjectType star { get { return types[2]; } }
        public static ObjectType Float { get { return types[3]; } }
        public static ObjectType Integer { get { return types[4]; } }
        public static ObjectType String { get { return types[5]; } }
        public static ObjectType raft { get { return types[6]; } }

        private ObjectType(int idx, bool isArray, ObjectType_enum type)
        {
            this.stringValue = type.ToString() + (isArray ? "[]" : "");
            this.isArray = isArray;
            this.idx = idx;
        }
    }
    public enum ObjectType_enum
    {
        Undefined, cloud, bomb, star, Float, Integer, String, raft
    }

    public enum Function
    {
        Undefined, wait, spawn, despawn, choose, i, f, flood, array, select
    }

    public struct Command
    {
        public string variable;
        public Function function;
        public ObjectType subject_type;
        public string[] arguments;
        public Command(string variable, Function function, ObjectType subject_type, string[] args)
        {
            this.variable = variable;
            this.function = function;
            this.subject_type = subject_type;
            this.arguments = args;
        }
    }
    public static class _LevelParseUtil
    {
        public static bool IsIn<T>(this T a, params T[] bs)
        {
            foreach (var b in bs)
                if (a.Equals(b)) return true;
            return false;
        }
    }

    public class Commands
    {
        public Command[] commands;
        public LevelState baseState;
        public Commands(Command[] commands, LevelState baseState)
        {
            this.commands = commands;
            this.baseState = baseState;
        }
    }

    public class LevelParse
    {
        
        static bool hasSubjectTypeSpecifier(Function f)
        {
            return f == Function.spawn || f == Function.choose || f == Function.array;
        }
        static string[] load_level_str(string fileName)
        {
            return System.IO.File.ReadAllLines(Application.dataPath + "/" + fileName);
        }

        public static Commands LoadLevel(string fileName)
        {
            string[] lines = load_level_str(fileName);
            LevelState baseState = new LevelState();
            Command?[] parseResRaw = new Command?[lines.Length];
            for (int i = 0; i < parseResRaw.Length; i++)
            {
                parseResRaw[i] = null;
                if (lines[i].StartsWith("$"))
                    parse_statevar(lines[i], ref baseState);
                else
                    parseResRaw[i] = parse_line(lines[i]);
            }

            List<Command> result = new List<Command>();
            foreach (var cmd in parseResRaw)
                if (cmd.HasValue)
                    result.Add(cmd.Value);
            return new Commands(result.ToArray(), baseState);
        }

        static void parse_statevar(string line, ref LevelState state)
        {
            string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length != 3 || words[1] != "=")
                return;
            if (words[0].ToLower() == "$health")
                state.Health = int.Parse(words[2]);
            else if (words[0].ToLower() == "$score")
                state.Score = int.Parse(words[2]);
            else if (words[0].ToLower() == "$bosshealth")
            {
                int h = int.Parse(words[2]);
                state.BossHealth = h;
                state.BossMaxHealth = h;
            }
            else if (words[0].ToLower() == "$length")
                state.Length = int.Parse(words[2]);
            
        }

        static Command? parse_line(string line)
        {
            line = line.Trim();
            if (line.StartsWith("//"))
                return null;
            string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
                return null;
            if (words[0] == "var")
            {
                //var name = command arg1 arg2
                string[] cmd = words.Skip(3).ToArray();
                if (words[2] != "=")
                    return null;
                string variable = words[1];
                return parse_words(cmd, variable);
            }
            else
                //command arg1 arg2
                return parse_words(words);
        }

        static Command? parse_words(string[] words, string variable = null)
        {
            if (words[0].StartsWith("\""))
            {
                string[] s = new string[] { "" };
                foreach (var word in words)
                    s[0] += word + " ";
                s[0] = s[0].Remove(s[0].Length - 1);
                if (s[0][s[0].Length - 1] != '"')
                    return null;
                s[0] = s[0].Remove(s[0].Length - 1);
                s[0] = s[0].Substring(1);
                return new Command(variable, Function.Undefined, ObjectType.String, s);
            }
            else
            {
                Function function;
                if (words[0] == "Undefined")
                    return null; //security measure
                try { function = (Function)Enum.Parse(typeof(Function), words[0]); }
                catch (ArgumentException) { return null; }
                ObjectType subject_type = null;
                if(function == Function.i)
                {
                    subject_type = ObjectType.Integer;
                    function = Function.Undefined;
                }
                else if(function == Function.f)
                {
                    subject_type = ObjectType.Float;
                    function = Function.Undefined;
                }
                
                bool skip1 = hasSubjectTypeSpecifier(function);
                if (skip1)
                {
                    try { subject_type = ObjectType.Parse(words[1]); }
                    catch (ArgumentException) { return null; }
                }
                string[] args = words.Skip(skip1 ? 2 : 1).ToArray();
                return new Command(variable, function, subject_type, args);
            }
        }

    }
}
