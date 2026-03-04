# TSD Full Stack CMS Technical Assessment
A Short technical assessment to test out ones ability to create a basic umbraco site with data driven from an API

## The Ask

We want you to take this shell solution, that consists of an empty Umbraco 17 site, that has SaSS pre configured and a .net 10 web api, and create a basic output for the products supplied in the included products.json file in the root of the repository.

This should use Umbraco best practises to output the data in a format of your choosing that makes sense for the context of the data given.

The data should come from the API, which internally reads the products.json file as a stubbed "database call". How you implement the API is up to you.

There is also a media.zip file included in the base of the solution, that contains stock imagery that corresponds to the products should you need it.

Ideally, the content output from the umbraco site should be done in such a way that it is easy to configure for a non technical user in the CMS, should they decide they wanted to view the data on a different page
or area of the site.

## Notes

- Run `npm install` to install the required node packages from the WWW prroject root
- Run `npm run sass` to compile Sass once
- Run `npm run sass:watch`to watch and auto-compile on changes

Compiled css will be output in wwwroot/css for you to consume

If you are using Rider as your IDE, you should be able to run both the API and CMS using the "Run All" compound build target