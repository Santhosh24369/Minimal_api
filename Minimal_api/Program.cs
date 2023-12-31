using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal_api;
using Minimal_api.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<JobServices>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/Jobs", async Task<List<JobModel>> (JobServices Job) => {
    return await Job.GetAsync();
    });

app.MapGet("/Jobs/{id:length(24)}", async  (JobServices Job,string id) =>
{
    return await Job.GetAsync(id);
});

app.MapPost("/Jobs", async(JobServices Job, JobModel newJob)=>
{
    await Job.CreateAsync(newJob);
    return await Job.GetAsync(newJob.Id);
}
);

app.MapPut("/Jobs{id:length(24)}", async  (JobServices Job, JobModel updatedJob,string id) =>
{
    var res = await Job.GetAsync(id);
    if(res is null)
    {
        return "NotFound";
    }
    updatedJob.Id = res.Id;

    await Job.UpdateAsync(id, updatedJob);

    return "Success";
});

app.MapDelete("/jobs/{id:length(24)}",async (JobServices Job,string id) =>
{
    var res = await Job.GetAsync(id);
    if (res is null)
    {
        return "NotFound";
    }
    await Job.RemoveAsync(id);

    return "Success";
} );
app.Run();
