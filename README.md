# WebLog

Yet another incarnation of my always experimental website genrrator.

These project is meant to take the least pragmatic but most interesting approach to making things work.

Right now I'm playing with Docker and all the newfangled cloud jazz. 
It may be possible to build a really simple effective yet flexible and decoupled solutuion.
Just need to avoid the temptation to abuse a tech for the wrong use.


## Phase 1 - Write basic code [Complete]

Made a markdown parsing site for ASP Core 3.

## Phase 2 - Publish basic code [Complete]

Shoved it in docker and launched it on Azure.

## Phase 3 - Make Updates Practical [In Progress]

The MD files are in a git repo. I was thinking of making it update by checking out the git repo.
But then I realised that this would be prone to all sorts of locking and sync issues.

__Epiphany: That is what databases were invented for.__

The following is a brain dump of what I'm going to do:

SQLServer is expensive overkill.

Question: TableStore or Cosmos? Either way I'll shove it behind an interface so I can lift and shift to AWS.

It isn't really a web servers job to create static pages, that's compute.
So somehow I want to:

1. Detect changes in a git repo (still want to write and push new posts via git)
2. Use _cloud compute_ to queue updates to each affected page
3. The queue is one of these newfangled _cloud queues_
4. A _cloud compute_ thing consumes pages from the queue, generates the HTML + rebuilds archive (only the afected quarter)
5. Stores results in Cosmos or somthing simpler if that makes sense
6. Website serves directly from storage
    * Web server is now totally stateless, no assets in web project
    * Actually serves SASS and JS until I make another function to plonk them in the database too
    * Then the web server project will contain zero content - well apart from layout?
    * Actually thisis cool, all the parts of the website can self build independantly
    * Total separation between the serve, content, and structure, all build on different function queues
    
    

## Phase 4 - Caching

I can get away with no sane caching since I have no traffic.

* Use the simple ASP Core memory caching
* Try out Redis ()

## Phase 5 - AWS / GoogleCloud anywhere else

It'll be a bunch of Docker containers that magically on another cloud provider...
