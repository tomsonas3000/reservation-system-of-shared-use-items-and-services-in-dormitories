export const handleErrors = (formik: any, err: any) => {
  const errors = {} as { [key: string]: string };
  const responseErrors = err.response.data;

  Object.keys(responseErrors).map((item: string) => {
    errors[item] = responseErrors[item];
  });

  formik.setErrors(responseErrors);
};
