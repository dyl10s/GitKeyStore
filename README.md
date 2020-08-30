# GitKeyStore
GitKeyStore is a simple key value database using a GitHub repo for storage.

## Saving data
To save data make a post request to [https://dylancontainers.tk/?key={YOUR KEY}&value={YOUR VALUE}](<https://dylancontainers.tk/?key={YOUR%20KEY}&value={YOUR%20VALUE}>) writing to the GitHub repo required a 
pull request and a push so it can take a second or two.

## Retrieving Data
To get a key back from the "database" (GitHub repo) just make a get request to [https://dylancontainers.tk/{YOUR KEY}](<https://dylancontainers.tk/{YOUR KEY}>) this is much faster then saving the
data as it basically just navigates to the raw preview GitHub already provides.

## View the database
The state of the art database management tool can be found here: https://dylancontainers.tk/View/Repo
