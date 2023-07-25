using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;

namespace TestApp.TagHelpers
{
    /// <summary>
    /// convert "<protected class="protected-str">My sensitive data</protected>" 
    /// to "<a href="#" data-protected="195$8ebae3b0a6adb0aab7aab5a6e3a7a2b7a2" class="protected-str">[Protected]</a>"
    /// </summary>
    [HtmlTargetElement("protected", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ProtectedTagHelper : TagHelper
    {
        /// <summary>
        /// Default value for placeholder in source code before decoding by JS
        /// </summary>
        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; } = "[Protected]";
        /// <summary>
        /// Page to open on click on Tag before decoding by JS
        /// </summary>
        [HtmlAttributeName("href")]
        protected virtual string LinkTarget { get; } = "#";

        /// <summary>
        /// formate Tag and encode content
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = (await output.GetChildContentAsync()).GetContent();

            var encoded = EncodeString(content, Random.Shared.Next(1, 256));

            output.TagName = "a";
            output.Attributes.RemoveAll("placeholder");
            output.Attributes.SetAttribute("data-protected", encoded);

            output.Content.SetContent(Placeholder);
        }

        /// <summary>
        /// Encode the string with the key with XOR
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns>formated encrypted string</returns>
        protected static string EncodeString(string str, int key)
        {
            var encrypted = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                var charCode = (int)str[i];
                var encryptedChar = charCode ^ key;
                encrypted.Append(encryptedChar.ToString("x2"));
            }

            return key + "*" + encrypted.ToString();
        }
    }
}
