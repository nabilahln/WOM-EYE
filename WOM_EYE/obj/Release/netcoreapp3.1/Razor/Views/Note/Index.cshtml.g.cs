#pragma checksum "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48888987f3a86a83f992b141d52277379e5127d8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Note_Index), @"mvc.1.0.view", @"/Views/Note/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\_ViewImports.cshtml"
using WOM_EYE;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\_ViewImports.cshtml"
using WOM_EYE.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48888987f3a86a83f992b141d52277379e5127d8", @"/Views/Note/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87e363bf627c603b7fa11bcf3782505af8aa76b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Note_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WOM_EYE.Models.Notes.NoteModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/vendor/datatables/dataTables.bootstrap4.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Note", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
  
    ViewData["Title"] = "Catatan " + ViewBag.namaTahap;
    Layout = "~/Views/Shared/_Layout.cshtml";
    ViewData["responseCode"] = Model.responseCode;

    string idButtonEditNote = "";
    List<String> listButtonEditNote = new List<String>();

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- #region Area Response -->\r\n<div>\r\n");
            WriteLiteral("</div>\r\n\r\n<h3>Notes ");
#nullable restore
#line 34 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     Write(ViewBag.namaTahap);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n<hr />\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48888987f3a86a83f992b141d52277379e5127d86243", async() => {
                WriteLiteral("\r\n\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "48888987f3a86a83f992b141d52277379e5127d86513", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<div>\r\n");
#nullable restore
#line 43 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     if (ViewBag.detailProject.IS_ACCESS == "TRUE")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48888987f3a86a83f992b141d52277379e5127d88666", async() => {
                WriteLiteral("\r\n            <!-- Input ID-->\r\n            <input type=\"hidden\" name=\"uid\" id=\"uid\"");
                BeginWriteAttribute("value", " value=\"", 1410, "\"", 1468, 1);
#nullable restore
#line 47 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
WriteAttributeValue("", 1418, ViewBag.detailProject.T_WOMEYE_DETAIL_PROJECT_UID, 1418, 50, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
            <button class=""btn btn-success btn-icon-split"">
                <span class=""icon text-white-50"">
                    <i class=""fas fa-solid fa-plus""></i>
                </span>
                <span class=""text"">Add Data</span>
            </button>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 55 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
<br />

<div class=""card shadow mb-4"">
    <div class=""card-body"">
        <table class=""table table-bordered"" id=""dataTable"">
            <thead>
                <tr>
                    <th>NO </th>
                    <th>USER NAME</th>
                    <th>STATUS</th>
                    <th>NOTE</th>
                    <th>DATE CREATE</th>
                    <th>ACTION</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 73 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                 for (int i = 1; i < Model.ListCatatan.Count; i++)
                {
                    idButtonEditNote = "buttonEditId" + Model.ListCatatan[i].M_WOMEYE_CATATAN_ID;
                    listButtonEditNote.Add(idButtonEditNote);


#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n");
            WriteLiteral("                        <td>");
#nullable restore
#line 80 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                       Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 81 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                       Write(Html.DisplayFor(model => model.ListCatatan[i].USR_CRT));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 82 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                       Write(Html.DisplayFor(model => model.ListCatatan[i].STATUS_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 83 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                       Write(Html.DisplayFor(model => model.ListCatatan[i].NOTES));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 84 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                       Write(Html.DisplayFor(model => model.ListCatatan[i].DTM_CRT));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n");
#nullable restore
#line 86 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                             if (Model.ListCatatan[i].IS_EDIT == "TRUE")
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48888987f3a86a83f992b141d52277379e5127d814343", async() => {
                WriteLiteral("\r\n                                    <input type=\"hidden\" name=\"id\" id=\"id\"");
                BeginWriteAttribute("value", " value=\"", 3325, "\"", 3400, 1);
#nullable restore
#line 89 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
WriteAttributeValue("", 3333, Html.DisplayFor(model => model.ListCatatan[i].M_WOMEYE_CATATAN_ID), 3333, 67, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"border-image-width:auto\" />\r\n                                    <button class=\"btn btn-warning btn-sm \"><i class=\"fa fa-edit\"></i>Edit</button>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 92 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 96 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>


<script src=""vendor/jquery/jquery.min.js""></script>

<!-- Page level plugins -->
<script src=""vendor/datatables/jquery.dataTables.min.js""></script>
<script src=""vendor/datatables/dataTables.bootstrap4.min.js""></script>

<!-- Page level custom scripts -->
<script src=""js/demo/datatables-demo.js""></script>

<script>
    var accessAdd = false;
    var accessEdit = false;

    var sol_leader = '");
#nullable restore
#line 116 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                 Write(ViewBag.project.SOL_LEADER);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n    var project_leader = \'");
#nullable restore
#line 117 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                     Write(ViewBag.project.PROJECT_LEADER);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n    var userLogin = \'");
#nullable restore
#line 118 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                Write(ViewBag.UserLogin);

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n    var listNote = [[]];\r\n    var listProgrammer = [];\r\n    var listFA = []\r\n    var listButton = [];\r\n    var loop = 1;\r\n\r\n");
#nullable restore
#line 125 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     for(int i = 1; i < Model.ListCatatan.Count; i++)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            WriteLiteral("listNote.push([]);\r\n    ");
            WriteLiteral("console.log(\'");
#nullable restore
#line 128 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
              Write(Model.ListCatatan[i].USR_CRT);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 129 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"

			}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 132 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     for(int i = 1; i < Model.ListCatatan.Count; i++)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            WriteLiteral("listNote[\'");
#nullable restore
#line 134 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
               Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\'].push(\'buttonEditId\'+\'");
#nullable restore
#line 134 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                                         Write(Model.ListCatatan[i].M_WOMEYE_CATATAN_ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n        ");
            WriteLiteral("listNote[\'");
#nullable restore
#line 135 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
               Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\'].push(\'");
#nullable restore
#line 135 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                          Write(Model.ListCatatan[i].USR_CRT);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 136 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"

			}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 139 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     foreach(var programmer in ViewBag.listProgrammer)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            WriteLiteral("listProgrammer.push(\'");
#nullable restore
#line 141 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                              Write(programmer);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 142 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 144 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     foreach(var fa in ViewBag.listFA)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            WriteLiteral("listFA.push(\'");
#nullable restore
#line 146 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                      Write(fa);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 147 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 149 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
     foreach(var button in listButtonEditNote)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            WriteLiteral("listButton.push(\'");
#nullable restore
#line 151 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                          Write(button);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 152 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    if (userLogin == sol_leader) {
        accessAdd = true;
    }
    else if (userLogin == project_leader) {
        accessAdd = true;
    }
    else {
        for (let i = 0; i < listProgrammer.length; i++) {
            console.log(listProgrammer[i]);
            if (userLogin == listProgrammer[i]) {
                accessAdd = true;
            }
        }

        for (let j = 0; j < listFA.length; j++) {
            console.log(listFA[j]);
            if (userLogin == listFA[j]) {
                accessAdd    = true;
            }
        }
    }

    for (let i = 1; i < '");
#nullable restore
#line 176 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                    Write(Model.ListCatatan.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'; i++) {
        console.log(listNote[i][0]);
        if (userLogin == listNote[i][1]) {
            document.getElementById(listNote[i][0]).style.visibility = 'visible';
            console.log('Button set visible' + listNote[i][0]);

        } else {
            console.log('Button set hidden' + listNote[i][0]);
            document.getElementById(listNote[i][0]).style.visibility = 'hidden';
        }
    }

    if (accessAdd == false) {
        document.getElementById('btnAddNote').style.visibility = 'hidden';
    } else {
        document.getElementById('btnAddNote').style.visibility = 'visible';

    }

    console.log(listNote[1]);
    console.log(listProgrammer);
    console.log(listFA);
    console.log(listButton);
    console.log(project_leader);
    console.log(sol_leader);
    console.log(userLogin);

    console.log('Access of ' + userLogin + ' is ' + accessAdd);

</script>

<script type=""text/javascript"">
        function openSuccessModal(strMessage) {
          ");
            WriteLiteral(@"  var myDiv = document.getElementById(""MyModalSuccessAlertBody"");
            myDiv.innerHTML = strMessage;
            $('#myModalSuccess').modal('show');
        }

        function openErrorModal(strMessage) {
            var myDiv = document.getElementById(""MyModalErrorAlertBody"");
            myDiv.innerHTML = strMessage;
            $('#myModalError').modal('show');
        }

        $(document).ready(function () {
            var code = """);
#nullable restore
#line 221 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                   Write(TempData["MyResponseCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n            var msg = \"");
#nullable restore
#line 222 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                  Write(TempData["MyResponseMessage"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""";
            if (code == ""200"") {
                //openSuccessModal(msg);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: msg
                });
            } else {
                if (");
#nullable restore
#line 231 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
               Write(TempData["MyResponseCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" != null && ");
#nullable restore
#line 231 "D:\Nabilah\WOM-EYE\Deployed\27 april\WOM_EYE\WOM_EYE\Views\Note\Index.cshtml"
                                                      Write(TempData["MyResponseCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" != ""xx"")
                    //openErrorModal(""Update Failed: "" + msg);
                Swal.fire({
                    icon: 'error',
                    title: 'Update Failed',
                    text: msg
                });
            }
        });
</script>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WOM_EYE.Models.Notes.NoteModel> Html { get; private set; }
    }
}
#pragma warning restore 1591