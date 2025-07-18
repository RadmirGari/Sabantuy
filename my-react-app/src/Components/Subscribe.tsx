import { useEffect, useState } from "react";
import { subscribe } from "../Services/subscriberService";

function Subscribe() {
  const [email, setEmail] = useState<string>("");
  const [name, setName] = useState<string>("");
  const [err, setErr] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleSubmit = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault()

    setErr(null);
    setSuccess(null);
    
    if(!email){
      setErr("No email was set. Please set your email and try again.");
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      setErr("Invalid email format. Please enter a valid email address.");
      return;
    }

    if(!name){
      setErr("No name was set. Please set your name and try again.");
      return;
    }

    try{
      await subscribe(email, name);
      setErr(null);
      setSuccess("Successfully subscribed to the email service!");
    } catch (err: any){
      setErr(err.message || "There was an error subscribing to the email service. Please try again later.");
    } finally{
      setName("");
      setEmail("");
    }
  }
  
  useEffect(() => {
    if (!err) return;
    const timer = setTimeout(() => setErr(null), 5000);
    return () => clearTimeout(timer);
  }, [err]);

  useEffect(() => {
    if (!success) return;
    const timer = setTimeout(() => setSuccess(null), 5000);
    return () => clearTimeout(timer);
  }, [success]);

  return(
    <>
      {err && (
        <>
          <div className="text-red-600 text-center font-bold px-2 py-2 rounded-full">
            Error: {err}
          </div>
        </>
      )}

      {success && (
        <>
          <div className="text-green-600 text-center font-bold px-2 py-2 rounded-full">
            Success: {success}
          </div>
        </>
      )}
      <div id="subscribe-form" className="mt-20 p-10 bg-gray-100 flex justify-center">
        <div className="w-full max-w-md lg:w-1/3">
          <h2 className="text-2xl font-bold mb-4 text-center">Subscribe to Our Newsletter</h2>
          <form>
          <input
              type="text"
              placeholder="Email"
              value={email}
              onChange={e => setEmail(e.currentTarget.value)}
              className="block w-full mb-4 p-2 border border-gray-300 rounded"
            />
           <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={e => setName(e.currentTarget.value)}
              className="block w-full mb-4 p-2 border border-gray-300 rounded"
            />
            <button
              className="bg-red-400 text-black font-semibold px-6 py-3 rounded-full hover:bg-red-300 transition w-full"
              onClick={e => handleSubmit(e)}
            >
              Submit
            </button>
          </form>
        </div>
      </div>
    </>
  )
}

export default Subscribe;