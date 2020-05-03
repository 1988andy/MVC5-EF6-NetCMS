using BLL;
using IBLL;
using IBLL.Model;
using IBLL.Model.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTest.Controllers
{
    /// <summary>
    /// 作者：王成龙 (1988Andy)
    /// 时间：2020-04-28
    /// 描述：MVC5 + EF6 增删改查Demo程序 Code First
    /// </summary>
    public class TestController : Controller
    {
        ITestService service = new TestService();
        //
        // GET: /Test/
        public ActionResult Index(ArticleRequest request)
        {
            //获取分类
            ChannelRequest channelRequest = new ChannelRequest() { ParentId = -1 };
            var channelResult = service.GetChannelList(channelRequest);
            //这里可以改造成注入方式
            //var channelResult = this.CmsService.GetChannelList(channelRequest);

            //获取分类树
            List<TreeNode> resultTree = new List<TreeNode>();
            this.GetChannelTree(channelResult, resultTree, 0);
            if (resultTree.Count > 0)
            {
                request.ChannelIds = resultTree.Select(t => t.ID).ToList();
            }

            foreach (var item in resultTree)
            {
                for (var i = 1; i <= item.level; i++)
                {
                    item.Name = "－" + item.Name;
                }
            }

            ViewBag.ChannelId = new SelectList(resultTree.AsEnumerable(), "ID", "Name");
            var result = service.GetArticleList(request);

            return View(result);
        }

        //
        // GET: /Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Test/Create
        public ActionResult Create()
        {
            this.ViewBag.Tags = service.GetTagList(new TagRequest() { Top = 20, Orderby = Orderby.Hits });
            var model = new Article() { IsActive = true, IsProposed = true };
            //获取分类
            ChannelRequest request = new ChannelRequest() { ParentId = -1, IsActive = true };
            var result = service.GetChannelList(request);

            List<TreeNode> resultTree = new List<TreeNode>();
            this.GetChannelTree(result, resultTree, 0);
            foreach (var item in resultTree)
            {
                for (var i = 1; i <= item.level; i++)
                {
                    item.Name = "－" + item.Name;
                }
            }
            ViewBag.ChannelId = new SelectList(resultTree.AsEnumerable(), "ID", "Name");
            return View("Edit", model);
        }

        //
        // POST: /Test/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new Article();
                this.TryUpdateModel<Article>(model);
                if (model.Content == null)
                {
                    model.Content = "&nbsp;";
                }
                Article article = null;
                service.SaveArticle(model, out article);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Test/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetArticle(id);
            var cmodel = service.GetChannel(model.ChannelId);
            //获取分类
            ChannelRequest request = new ChannelRequest() { ParentId = -1 };
            var result = service.GetChannelList(request);

            List<TreeNode> resultTree = new List<TreeNode>();
            this.GetChannelTree(result, resultTree, 0);
            foreach (var item in resultTree)
            {
                for (var i = 1; i <= item.level; i++)
                {
                    item.Name = "－" + item.Name;
                }
            }
            ViewBag.ChannelId = new SelectList(resultTree.AsEnumerable(), "ID", "Name", cmodel.ID.ToString());
            this.ViewBag.Tags = service.GetTagList(new TagRequest() { Top = 20, Orderby = Orderby.Hits });

            return View(model);
        }

        //
        // POST: /Test/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var model = service.GetArticle(id);
                this.TryUpdateModel<Article>(model);
                service.SaveArticle(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        ////
        //// POST: /Test/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    if (id > 0)
        //    {
        //        service.DeleteArticle(id);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //批量删除
        // POST: /Test/Delete/5
        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var item = service.GetArticle(id);
                    if (!string.IsNullOrWhiteSpace(item.File) && System.IO.File.Exists(Server.MapPath(item.File)))
                    {
                        System.IO.File.Delete(Server.MapPath(item.File));
                    }
                }
                service.DeleteArticle(ids);

                return Json(new { Url = Url.Action("Index"), result = 1 });
            }
            else
            {
                return Json(new { Url = Url.Action("Index"), result = 0 });
            }
        }

        /// <summary>
        /// 获取分类Tree
        /// </summary>
        /// <param name="channels">原频道分类对象</param>
        /// <param name="newCategorys">Tree对象</param>
        /// <param name="parentId">父类ID</param>
        /// <param name="lv">频道层级</param>
        public void GetChannelTree(IEnumerable<Channel> channels, List<TreeNode> newCategorys, int parentId = 0, int lv = 0)
        {
            foreach (var item in channels.Where(c => c.ParentId == parentId))
            {
                newCategorys.Add(new TreeNode()
                {
                    ID = item.ID,
                    level = lv,
                    parentID = item.ParentId,
                    Name = item.Name,
                    Class = parentId == 0 ? "icon-folder-open" : channels.Any(c => c.ParentId == item.ID) ? "icon-minus-sign" : "icon-leaf"
                });

                GetChannelTree(channels, newCategorys, item.ID, lv + 1);
            }
        }
    }
}
