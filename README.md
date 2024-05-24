# NutNut - a streaming website (that doesn't actually stream)

This was our 5th semester final project (dotnet and blazor pages). It does all the warm stuff like:
- Sign in
- Sign out
- Sign up
- Sign up for premium (yeah they're differently implemented)
- Check for valididty of the premium membership (it's a monthly subscription)
- Show movies/tv shows from a local database (**MSSQL**) managed by **C#**
- Show the watch page to only a premium member (free users are redirected to sign up for a premium page)

There may be other features too. Don't really remember anymore.

If you want to use this or just check it out, make sure that you replace all the occurrences of the `connectionString` (should've made that a global static but this along with lots of other parts were supposed to be cleaned and managed properly but after the reward ceremony {see below}, we simply didn't have the love to complete its other features). Then also make sure that your database has the same shape because you know how it is. At the time of publishing it here, I've lost the database schema files and everything, but I got that *sql* file for movies data but the tv show data is lost. It can be downloaded from **kaggle**. You will also need to clean that data using some kind of script (we used python and *pandas*)

If you just want to take a preview, you can take a look at it on my YouTube channel: [click here](https://youtu.be/0rrFsoqRJmY)

-------

### Reward

We were presented with the following for making this project in just 3 days:
> Where did you copy this from?
> (translated from Urdu)

It was very motivational for us.

##### Thanks for readmeing
