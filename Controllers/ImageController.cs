using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjaMVC.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AplikacjaMVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageController(ProjectDatabase _context, IWebHostEnvironment hostEnvironment)
        {
            this.Context = _context;
            this._hostEnvironment = hostEnvironment;
        }


        private ProjectDatabase Context { get; }
        public IActionResult Index()
        {
            return View("Upload");
        }

        public IActionResult Upload([FromForm] Images image)
        {
           var session = HttpContext.Session.GetString("_Name");
          if (session != null)
            {
                if (ModelState.IsValid)
                {
                    string Description = image.Description;
                    string Title = image.Title;

                    var fileName = Path.GetFileName(image.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    string filePath = "~/Uploads/" + fileName;
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string path = Path.Combine(wwwRootPath + "/Uploads/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        fileStream.Position = 0;
                        image.ImageFile.CopyToAsync(fileStream);
                    }



                    Images newImage = new Images();
                    newImage.Description = Description;
                    newImage.Title = Title;
                    newImage.Filename = fileName;
                    newImage.Filetype = fileExtension;
                    newImage.CreatedOn = DateTime.Now;
                    newImage.Path = filePath;

                    this.Context.Images.Add(newImage);
                    this.Context.SaveChanges();


                    return View("UploadSuccesfull");
                }
                else
                {
                    return View("UploadFail");
                }
            }
            else
            {
                TempData["ErrorUpload"] = "You need to have an account to upload.";
                return View("Upload");
            }
            

        }

        public IActionResult MainPage()
        {
            Images images = new Images();
            //var image = (from i in this.Context.Images select i).ToList();
            List<Images> image = (from i in this.Context.Images
                                         where i.Rating >= 1
                                         select i).ToList();

            return View(image);
        }



        public IActionResult WaitingPage()
        {
            Images images = new Images();
            //var image = (from i in this.Context.Images select i).ToList();
            List<Images> waitingImage = (from i in this.Context.Images
                                  where i.Rating < 1
                                  select i).ToList();

            return View(waitingImage);
        }
        public ActionResult ImagePage(int id, string title)
        {
            int ImageId = id;
            Images image = new Images();
            image = Context.Images
                   .Where(m => m.Id == ImageId)
                    .Select(m => m)
                   .SingleOrDefault();
            

            return View(image);
        }

        public ActionResult ParitialImage(int id, string title)
        {
            int ImageId = id;
            Images image = new Images();
            image = Context.Images
                   .Where(m => m.Id == ImageId)
                    .Select(m => m)
                   .SingleOrDefault();

            return PartialView(image);
        }


        public int ItemByIdFinder(int id)
        {

            var item = Context.Images
                   .Where(m => m.Id == id)
                    .Select(m => m.Rating)
                   .SingleOrDefault();
            return item;

        }




            [HttpPost]
        public ActionResult Buttons(string plus, string minus)
        {


            if (HttpContext.Session.Keys != null)
            {
                string ImageId;
                string Username = HttpContext.Session.GetString("_Name");
                int UserID = Context.Register
                   .Where(m => m.Username == Username)
                    .Select(m => m.Id)
                   .SingleOrDefault();
                


                if (plus == null)
                {
                    //sprawdzenie czy na minus
                    var madeVote = Context.ImagesVotes
                       .Where(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)))
                        .Select(m => m.Vote)
                       .SingleOrDefault();

                    if (madeVote == "minus")
                    {
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();

                        return PartialView("MainPage",image);
                    }
                    else if (madeVote == "plus")
                    {
                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(minus) };
                        var dataPerson = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)));

                        dataPerson.Vote = "NULL";
                        ImageId = minus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating - 1;
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(dataPerson).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("MainPage",image);
                    }
                    else if (madeVote == "NULL")
                    {
                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(minus) };
                        var dataPerson = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)));

                        dataPerson.Vote = "minus";
                        ImageId = minus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating - 1;
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(dataPerson).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("MainPage", image);
                    }
                    
                   
                }
                else
                {
                   
                    var madeVote = Context.ImagesVotes
                       .Where(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(plus)))
                        .Select(m => m.Vote)
                       .SingleOrDefault();


                    if (madeVote == "plus")
                    {
                        
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();

                        return PartialView("MainPage",image);


                      
                    }
                    else if (madeVote == "minus")
                    {
                        //podmianka aminusa na plusa


                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(plus) };
                        var daneOsoby = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(plus)));

                        daneOsoby.Vote = "NULL";
                        ImageId = plus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating + 1;
                        //this.Context.ImagesVotes.Update(doneVote);
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(daneOsoby).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("MainPage", image);

                    }
                    else
                    {
                        //glos na plus
                        ImageId = plus;
                        string vote = "plus";
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating + 1;

                        ImagesVotes imagesVotes = new ImagesVotes();
                        imagesVotes.ImagesID = id;
                        imagesVotes.RegisterID = UserID;
                        imagesVotes.Vote = vote;

                        this.Context.ImagesVotes.Add(imagesVotes);
                        Images image = new Images();
                        image = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m)
                           .SingleOrDefault();
                        this.Context.SaveChanges();
                        List<Images> imaged = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        return PartialView("MainPage", imaged);
                    }

                    
                }



            }
            else {
                throw new Exception($"SESSION NOT SET");
            }
           
            

            


           
            return RedirectToAction("MainPage");
        }


        [HttpPost]
        public ActionResult ButtonsPage(string plus, string minus)
        {


            if (HttpContext.Session.Keys != null)
            {
                string ImageId;
                string Username = HttpContext.Session.GetString("_Name");
                int UserID = Context.Register
                   .Where(m => m.Username == Username)
                    .Select(m => m.Id)
                   .SingleOrDefault();



                if (plus == null)
                {
                    //sprawdzenie czy na minus
                    var madeVote = Context.ImagesVotes
                       .Where(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)))
                        .Select(m => m.Vote)
                       .SingleOrDefault();

                    if (madeVote == "minus")
                    {
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();

                        return PartialView("ImagePage", image);
                    }
                    else if (madeVote == "plus")
                    {
                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(minus) };
                        var dataPerson = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)));

                        dataPerson.Vote = "NULL";
                        ImageId = minus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating - 1;
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(dataPerson).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("ImagePage", image);
                    }
                    else if (madeVote == "NULL")
                    {
                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(minus) };
                        var dataPerson = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(minus)));

                        dataPerson.Vote = "minus";
                        ImageId = minus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating - 1;
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(dataPerson).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("ImagePage", image);
                    }


                }
                else
                {
                    //sprawdzenie czy glos byl na plus
                    var madeVote = Context.ImagesVotes
                       .Where(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(plus)))
                        .Select(m => m.Vote)
                       .SingleOrDefault();


                    if (madeVote == "plus")
                    {
                        //error double plus
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();

                        return PartialView("ImagePage", image);


                        //TempData["ErrorDoubleDownVote"] = "You cannot upvote twice";
                        //return RedirectToAction("MainPage");

                    }
                    else if (madeVote == "minus")
                    {
                        //podmianka aminusa na plusa


                        var doneVote = new ImagesVotes { RegisterID = UserID, ImagesID = Int32.Parse(plus) };
                        var daneOsoby = this.Context.ImagesVotes.FirstOrDefault(m => m.RegisterID == UserID && (m.ImagesID == Int32.Parse(plus)));

                        daneOsoby.Vote = "NULL";
                        ImageId = plus;
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating + 1;
                        //this.Context.ImagesVotes.Update(doneVote);
                        List<Images> image = (from i in this.Context.Images
                                              where i.Rating >= 1
                                              select i).ToList();
                        Context.Entry(daneOsoby).State = EntityState.Modified;
                        Context.SaveChanges();
                        return PartialView("ImagePage", image);

                    }
                    else
                    {
                        //glos na plus
                        ImageId = plus;
                        string vote = "plus";
                        int id = Int32.Parse(ImageId);
                        int Rating = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m.Rating)
                           .SingleOrDefault();
                        var Ratings = Context.Images.Find(id);
                        Ratings.Rating = Ratings.Rating + 1;

                        ImagesVotes imagesVotes = new ImagesVotes();
                        imagesVotes.ImagesID = id;
                        imagesVotes.RegisterID = UserID;
                        imagesVotes.Vote = vote;

                        this.Context.ImagesVotes.Add(imagesVotes);
                        Images image = new Images();
                        image = Context.Images
                           .Where(m => m.Id == id)
                            .Select(m => m)
                           .SingleOrDefault();
                        this.Context.SaveChanges();
                        List<Images> imaged = (from i in this.Context.Images
                                               where i.Rating >= 1
                                               select i).ToList();
                        return PartialView("ImagePage", imaged);
                    }


                }



            }
            else
            {
                throw new Exception($"SESSION NOT SET");
            }







            return RedirectToAction("MainPage");
        }


    }
}

