using System;

namespace CSharpTestProject
{
    public class GetAnimAndTextureName : IStartable
    {
        private (string tex, byte ver) GetTextureNamenadVersion(string name)
        {
            int braceIndex = name.IndexOf("(");

            if (braceIndex < 0) return (GetProperName(name), 0);

            string properNmae = GetProperName(name.Substring(0, braceIndex));
            byte version = Convert.ToByte(name.Substring(braceIndex + 1, name.IndexOf(")") - braceIndex - 1));

            return (properNmae, version);
        }

        private string GetProperName(string name)
        {
            const char SPACEBAR = (char)32;
            string newName = String.Empty;

            foreach (string word in name.Split(new char[1] { SPACEBAR }, StringSplitOptions.RemoveEmptyEntries))
            {
                newName += word;
            }

            return newName;
        }

        public void Start()
        {
            string anim1 = "Forge";
            string anim2 = "Royal Chamber";
            string tex1 = "Countryside";
            string tex2 = "Woods(1)";
            string tex3 = "Royal Chamber(13)";

            Console.WriteLine($"Anim \"{anim1}\" become \"{GetProperName(anim1)}\"");
            Console.WriteLine($"Anim \"{anim2}\" become \"{GetProperName(anim2)}\"");
            var (tex, ver) = ((string)null, (byte)0);
            (tex, ver) = GetTextureNamenadVersion(tex1); Console.WriteLine($"Tex \"{tex1}\" become \"{tex}\", ver {ver}");
            (tex, ver) = GetTextureNamenadVersion(tex2); Console.WriteLine($"Tex \"{tex2}\" become \"{tex}\", ver {ver}");
            (tex, ver) = GetTextureNamenadVersion(tex3); Console.WriteLine($"Tex \"{tex3}\" become \"{tex}\", ver {ver}");
        }
    }
}
