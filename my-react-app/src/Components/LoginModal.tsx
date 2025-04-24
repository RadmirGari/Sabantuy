import React, { useState } from 'react';

interface LoginModalProps {
  setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
}

const LoginModal: React.FC<LoginModalProps> = ({ setLoggedIn }) => {
  const [password, setPassword] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await fetch(
        `http://localhost:5067/api/Section/check/${encodeURIComponent(password)}`,
        { method: 'GET' }
      );
      if (res.ok) {
        setLoggedIn(true);
      } else {
        console.warn('Invalid password');
      }
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 backdrop-blur-sm z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-80">
        <h2 className="text-xl font-semibold mb-4">Please Log in</h2>
        <form onSubmit={handleSubmit}>
          <label className="block mb-4">
            <span className="block mb-1">Password:</span>
            <input
              type="password"
              value={password}
              onChange={e => setPassword(e.target.value)}
              className="w-full px-3 py-2 border rounded focus:outline-none focus:ring"
            />
          </label>
          <button
            type="submit"
            className="w-full py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
          >
            Submit
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginModal;
