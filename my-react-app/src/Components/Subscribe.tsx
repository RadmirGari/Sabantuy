import { useEffect, useState } from "react";

function Subscribe() {
  const [email, setEmail] = useState<String | null>(null);
  const [name, setName] = useState<String | null>(null);
  const [err, setErr] = useState<String | null>(null);

  const handleSubmit = () => {
    if(!email){
      setErr("No email was set. Please set your email and try again.");
      return;
    }

    if(!email){
      setErr("No name was set. Please set your name and try again.");
      return;
    }

    try{
      
    } catch (err: any){
      setErr("There was an error subscribing to the email service. Please try again later.");
    }
  }

  return(
      <div id="subscribe-form" className="mt-20 p-10 bg-gray-100 flex justify-center">
      <div className="w-full max-w-md lg:w-1/3">
        <h2 className="text-2xl font-bold mb-4 text-center">Subscribe to Our Newsletter</h2>
        <form>
          <input
            type="text"
            placeholder="Name"
            className="block w-full mb-4 p-2 border border-gray-300 rounded"
          />
          <input
            type="email"
            placeholder="Email"
            className="block w-full mb-4 p-2 border border-gray-300 rounded"
            onInput={setEmail(e.value)}
          />
          <button
            type="submit"
            className="bg-red-400 text-black font-semibold px-6 py-3 rounded-full hover:bg-red-300 transition w-full"
            onClick={handleSubmit}
          >
            Submit
          </button>
        </form>
      </div>
    </div>
  )
}

export default Subscribe;