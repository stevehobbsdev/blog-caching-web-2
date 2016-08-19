<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CachingDemo.Web.Models.Data.Vehicle>" %>

<% using (Html.BeginForm()) {%>
    <%: Html.ValidationSummary(true) %>

	<%: Html.HiddenFor(m => m.Id) %>
           
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Name) %>
        <%: Html.ValidationMessageFor(model => model.Name) %>
    </div>
            
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Price) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.Price, String.Format("{0:F}", Model.Price)) %>
        <%: Html.ValidationMessageFor(model => model.Price) %>
    </div>
            
    <p>
        <input type="submit" value="Save" />
    </p>

<% } %>