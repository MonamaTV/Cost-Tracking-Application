<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Annoucement.aspx.cs" Inherits="School_Website.Annoucement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Annoucements | NSNP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <br />
            <div class="addMessage">
                <button type="button" runat="server" id="btnAnnoucement" class="btnRegister" data-toggle="modal" data-target="#ouraddmodal">Make Annoucement</button>
            </div>
             <div class="login-input">
                <div class="alert alert-success" role="alert" id="Alert" runat="server">
                    Successfully submitted your message
                </div>
            </div>
            <div class="login-input">
                <div class="alert alert-danger" role="alert" id="ErrorAlert" runat="server">
                    Could not submit the message. Try again!
                </div>
            </div>
            <div class="row" runat="server" id="MessagesCard">
                <br />
                <!-- Messages come here -->
            </div>
            <div class="modal fade" id="ouraddmodal" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-scrollable" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalTitle">Make Annoucement</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                     <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="login-form">
                                    <div class="login-input">
                                        <label for="subject">Subject:</label>
                                        <input type="text" name="subject" id="subject" runat="server">
                                    </div>
                                    <div class="login-input">
                                        <div class="filter">
                                            <asp:DropDownList id="messagetype" runat="server" class=" text-left"  >
                                                <asp:ListItem Text="Message Type" Value="1" />
                                                <asp:ListItem Text="Important" Value="1" />
                                                <asp:ListItem Text="Urgent" Value="2" />
                                                <asp:ListItem Text="Normal" Value="3" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="login-input">
                                        <label for="Message">Message:</label>
                                        <textarea name="message" id="message" cols="30" rows="10" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btnCancel" data-dismiss="modal">Close</button>--%>
                                <asp:Button ID="Button1" runat="server" class="btnCancel" Text="Cancel"  OnClick="btnCancel_Click"/>
                                <asp:Button ID="Button2" class="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click"/>
                                <%--<button type="button" class="btnSubmit" data-dismiss="modal">Submit</button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            <script>
                annoucement.className = 'active';

                const annouce = document.querySelector("#drop-icon");
                const deleteFunction = (id) => {              
                    if (confirm("Do you want to delete this message?")) {
                        //The rest is the back end
                        window.location.href = `Annoucement.aspx?delete=${id}`;
                    }
                    else {
                        window.location.href = "Annoucement.aspx";
                    }
                }     
                editButton.addEventListener('click', editMessage);
            </script>
            <script src="script/jquery.js"></script>
            <script src="script/bootstrap.js"></script>
            <script src="script/popper.min.js"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
