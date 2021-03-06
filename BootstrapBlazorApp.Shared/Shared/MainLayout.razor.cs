using BootstrapBlazor.Components;
using System.Collections.Generic;

namespace BootstrapBlazorApp.Shared.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 菜单获取可以通过数据库获取，此处为示例直接拼装的菜单集合
            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new MenuItem() {Text = "返回组件库", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components"},
                new MenuItem() {Text = "Index", Icon = "fa fa-fw fa-fa", Url = ""},
                new MenuItem() {Text = "Counter", Icon = "fa fa-fw fa-check-square-o", Url = "counter"},
                new MenuItem() {Text = "Goods", Icon = "fa fa-fw fa-check-square-o", Url = "goods"},
                new MenuItem() {Text = "FetchData", Icon = "fa fa-fw fa-database", Url = "FetchData"},
                new MenuItem() {Text = "Login", Icon = "fa fa-fw fa-database", Url = "login"},
                new MenuItem() {Text = "Register", Icon = "fa fa-fw fa-database", Url = "Register"},
                new MenuItem() {Text = "Admin User Manager", Icon = "fa fa-fw fa-database", Url = "admin-user-manage"},
                new MenuItem() {Text = "Customer Manage", Icon = "fa fa-fw fa-database", Url = "user-manage"},
                new MenuItem() {Text = "Order", Icon = "fa fa-fw fa-database", Url = "order"},
            };

            return menus;
        }
    }
}