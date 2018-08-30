using Microsoft.AspNetCore.Http;
using MuazaAngular.Models;
using MuazaAngular.Repo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MuazaWebApi.ProjectProcessing
{
    public class ProjectProcessing
    {
        DataContext _repo;
        public ProjectProcessing(DataContext repo)
        {
            _repo = repo;
        }
        public bool saveProject(Project _project, IFormFileCollection files,string path)
        {
            path = Path.Combine(path, "ProjectImages");
            _project.month = _project.start_date.ToString("MMMM");
            _repo.Add(_project);
            _repo.SaveChanges();
            List<string> test = new List<string>();
            List<string> url = new List<string>();

            if (!string.IsNullOrEmpty(_project.images))
            {
                test = JsonConvert.DeserializeObject<List<string>>(_project.images);
                url = JsonConvert.DeserializeObject<List<string>>(_project._url);


            }
            int count = 0;
            foreach (var t in files)
            {
                count++;
                var DirPath = Path.Combine(path, _project.id.ToString());
                if (!Directory.Exists(DirPath))
                    Directory.CreateDirectory(DirPath);
                var filepath = Path.Combine(DirPath,count.ToString()+t.Name);
                var relative_url = Path.Combine("ProjectImages", _project.id.ToString(),count.ToString()+t.Name);
                using (FileStream x = File.Open(filepath, FileMode.Create))
                {
                    t.CopyTo(x);
                }
                url.Add(relative_url);
                test.Add(filepath);
            }
            if(test.Count > 0)
            {
                _project.images = JsonConvert.SerializeObject(test);
                _project._url = JsonConvert.SerializeObject(url);


            }
            _repo.Update(_project);
            _repo.SaveChanges();
            return true;
        }
        public bool deleteProject(int id)
        {
            var _project = _repo.Projects.Where(x => id == x.id).SingleOrDefault();
            if (_project == null)
                return false;
            List<string> test = new List<string>();
            if (!string.IsNullOrEmpty(_project.images))
                test = JsonConvert.DeserializeObject<List<string>>(_project.images);
            string dir = string.Empty;
            foreach(var t in test)
            {
                try
                {
                    dir = Path.GetDirectoryName(t);
                    File.Delete(t);
                }
                catch
                {
                    continue;
                }
            }
            if(!string.IsNullOrEmpty(dir))
            Directory.Delete(dir);
            _repo.Projects.Remove(_project);
            _repo.SaveChanges();
            return true;
        }

        public IEnumerable<_Project> getProjects()
        {
            List<_Project> _projs = new List<_Project>();
            foreach( var project in _repo.Projects)
            {
                List<string> test = new List<string>();
                List <string> url = new List<string>();
                if (!string.IsNullOrEmpty(project.images))
                {
                     test = JsonConvert.DeserializeObject<List<string>>(project.images);
                     url = JsonConvert.DeserializeObject<List<string>>(project._url);
                }
                _projs.Add(new _Project { project = project, images = url });


            }

            return _projs ;
        }
        public _Project getProject(int id)
        {
            List<string> test = new List<string>();
            List<string> url = new List<string>();
            var p =  _repo.Projects.Where(x => x.id == id).SingleOrDefault();
            if (!string.IsNullOrEmpty(p.images))
            {
                test = JsonConvert.DeserializeObject<List<string>>(p.images);
                url = JsonConvert.DeserializeObject<List<string>>(p._url);
            }
            _Project proj = new _Project() { project = p, images = url };
            return proj;
        }
    }
}
