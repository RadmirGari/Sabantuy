import {URL} from '../Constants/URL';

export async function subscribe(email: string, name: string){
    //id is autoassigned in db once it inserts. So defaulting it to 0 here.
    const payload = {id: 0, name, email};

    const res = await fetch(`${URL}/api/Subscriber`,{
        method: "PUT",
        body: JSON.stringify(payload),
        headers: {"Content-Type": "application/json"},
    });

    const data = await res.json();

    //Status for a conflict
    if(res.status === 409){
        throw new Error("Email is already in use");
    }

    if(!res.ok){
        throw new Error("There was an error subscribing to the email service. Please try again later.");
    }

    return data;
}