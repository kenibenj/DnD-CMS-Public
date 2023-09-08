using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DnD_CMS.DAL;
using DnD_CMS.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DnD_CMS.Controllers
{
    public class CharacterController : Controller
    {

        CharacterDAL characterDAL = new CharacterDAL();
        IWebHostEnvironment webHostEnvironment;
        public CharacterController(IWebHostEnvironment _environment)
        {
            webHostEnvironment = _environment;
        }


        // GET: CharacterController

        /*
            Retrieves all characters and sends them to the Index view when user makes a GET request for the Index page
        */
        public ActionResult Index()
        {

            var characterList = characterDAL.getAllCharacters();

            if(characterList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently there are no characters in the database.";
            }

            return View(characterList);
        }


        // GET: CharacterController

        /*
            Retrieves all of the user's characters and sends them to the Index view when user makes a GET request for the MyCharacters page
        */
        public ActionResult MyCharacters()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUser)))
            {
                return RedirectToAction("Register", "User");
            }

            var characterList = characterDAL.getAllCharacters();

            return View(characterList);
        }


        // GET: CharacterController/Create

        /*
            Redirects user to register page if they are not logged in or sends them to the Create view when user makes a GET request for the Create page.
        */
        public ActionResult Create()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUser)))
            {
                return RedirectToAction("Register", "User");
            };
            return View();
        }


        // POST: CharacterController/Create

        /*
            When user submits character data from the Create page, the POST request sends this data to characterDAL and also stores the character picture to the wwwroot on the backend.
            Sends user back to Index page upon successful character creation
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(characterModel character)
        {
            bool IsInserted = false;
            if (string.IsNullOrWhiteSpace(webHostEnvironment.WebRootPath))
            {
                webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //Inserts character without picture in order to see what ID it will get, then deletes character and
                    //stores the ID + 1 as that is what the ID will be when the character is stored with picture.
                    IsInserted = characterDAL.insertCharacter(character);
                    var characterList = characterDAL.getAllCharacters();
                    var tempID = characterList.LastOrDefault().characterID;
                    characterDAL.deleteCharacter(tempID);
                    var characterID = tempID + 1;

                    //wwwrootpath to save file
                    string rootPath = webHostEnvironment.WebRootPath;
                    //Get uploaded file
                    var files = HttpContext.Request.Form.Files;

                    //Upload

                    if (files.Count != 0)
                    {
                        var imagePath = @"images\characters\";
                        var Extension = Path.GetExtension(files[0].FileName).ToLowerInvariant();
                        //Forbids non-image filetypes for character image
                        if (Extension == ".jpg" || Extension == ".png" || Extension == ".jpeg" || Extension == ".gif" || Extension == ".webp" )
                        {
                            var relativeImagePath = imagePath + characterID + Extension;
                            var absImagePath = Path.Combine(rootPath, relativeImagePath);

                            //Upload image
                            using (var fileStream = new FileStream(absImagePath, FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }

                            character.characterImage = relativeImagePath;
                        }
                    }
                    IsInserted = characterDAL.insertCharacter(character);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Character inserted successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Oops...Something went wrong.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }


        }

        // GET: CharacterController/Edit/5
        /*
            Redirects user to register page if they are not logged in or sends them to the Edit view when user makes a GET request for the Edit page.
        */
        public ActionResult Edit(int id)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUser)))
            {
                return RedirectToAction("Register", "User");
            };

            var characters = characterDAL.getCharacterByID(id).FirstOrDefault();

            if (characters == null)
            {
                TempData["InfoMessage"] = "Character not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(characters);
        }

        // POST: CharacterController/Edit/5

        /*
            When user submits character data from the Edit page, the POST request sends this data to characterDAL and also stores the character picture to the wwwroot on the backend.
            Sends user back to Index page upon successful character Edit
        */
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult updateCharacter(characterModel character)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Save Image
                    var characterID = character.characterID;

                    //wwwrootpath to save file
                    string rootPath = webHostEnvironment.WebRootPath;

                    //Get uploaded file
                    var files = HttpContext.Request.Form.Files;

                    //Upload
                    if (files.Count != 0)
                    {
                        var imagePath = @"images\characters\";
                        var Extension = Path.GetExtension(files[0].FileName).ToLowerInvariant();
                        //Forbids non-image filetypes for character image
                        if (Extension == ".jpg" || Extension == ".png" || Extension == ".gif" || Extension == ".jpeg" || Extension == ".webp")
                        {
                            var relativeImagePath = imagePath + characterID + Extension;
                            var absImagePath = Path.Combine(rootPath, relativeImagePath);


                            //Upload image
                            using (var fileStream = new FileStream(absImagePath, FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }
                            character.characterImage = relativeImagePath;
                        }
                    }

                    bool isUpdated = characterDAL.updateCharacter(character);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Character updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Oops...Something went wrong in the post editing process.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: CharacterController/Delete/5
        /*
            When user clicks delete button in the Edit page, the controller sends this data to the characterDAL and sends user to character Index page.
            Since the delete function is part of the Edit View, the Delete Action does not have its own view and does not need a GET method.
        */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = characterDAL.deleteCharacter(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
